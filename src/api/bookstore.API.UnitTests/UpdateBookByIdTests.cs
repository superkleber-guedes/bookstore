using bookstore.API.ComponentTests.Setup;
using bookstore.Infrastructure.Exceptions;
using bookstore.Integration;
using Microsoft.Azure.Cosmos;
using Newtonsoft.Json;
using NSubstitute;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace bookstore.API.ComponentTests
{
    public class UpdateBookByIdTests
    {
        private readonly IBookRepository _mockedBookRepository;
        private readonly ApiClientFixture _client;

        const string title = "Component Test Bible";
        const string author = "comp";
        const decimal price = 10;

        public UpdateBookByIdTests()
        {
            _mockedBookRepository = Substitute.For<IBookRepository>();
            _client = new ApiClientFixture(_mockedBookRepository);
        }

        [Fact]
        public async Task Post_UpdateBookById_Returns200()
        {
            // Arrange
            long id = 10001;
            _mockedBookRepository.GetByIdAsync(id).Returns(new Domain.Book(id, title, author, price));
            
            Kleber.Bookstore.Models.Book newBook = new Kleber.Bookstore.Models.Book()
            {
                Title = title,
                Author = author, 
                Price = price
            };

            //Act
            var defaultPage = await _client.SendAsync(System.Net.Http.HttpMethod.Put, $"/books/{id}", JsonConvert.SerializeObject(newBook));
            
            // Assert
            Assert.Equal(HttpStatusCode.OK, defaultPage.StatusCode);
            var content = await defaultPage.Content.ReadAsStringAsync();
            Assert.NotNull(content);

            await _mockedBookRepository.Received(1).GetByIdAsync(id);
            await _mockedBookRepository.Received(1).SaveAsync(Arg.Any<Domain.Book>());
        }

        [Fact]
        public async Task Post_UpdateBookById_ReturnsNotFound()
        {
            // Arrange
            long id = 10002;
            _mockedBookRepository
                .When(x => x.GetByIdAsync(id))
                .Do(x => { throw new ResourceNotFoundException(id, "Testing not found"); });

            Kleber.Bookstore.Models.Book newBook = new Kleber.Bookstore.Models.Book()
            {
                Title = title,
                Author = author,
                Price = price
            };

            //Act
            var defaultPage = await _client.SendAsync(System.Net.Http.HttpMethod.Put, $"/books/{id}", JsonConvert.SerializeObject(newBook));

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, defaultPage.StatusCode);
            await _mockedBookRepository.Received(1).GetByIdAsync(id);
            await _mockedBookRepository.Received(0).SaveAsync(Arg.Any<Domain.Book>());
        }

        [Fact]
        public async Task Post_UpdateBookById_EmptyTitle_ReturnsBadRequest()
        {
            // Arrange
            long id = 10003;
            _mockedBookRepository.GetByIdAsync(id).Returns(new Domain.Book(id, title, author, price));

            Kleber.Bookstore.Models.Book newBook = new Kleber.Bookstore.Models.Book()
            {
                Title = "",
                Author = author,
                Price = price
            };

            //Act
            var defaultPage = await _client.SendAsync(System.Net.Http.HttpMethod.Put, $"/books/{id}", JsonConvert.SerializeObject(newBook));

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, defaultPage.StatusCode);

            await _mockedBookRepository.Received(0).GetByIdAsync(id);
            await _mockedBookRepository.Received(0).SaveAsync(Arg.Any<Domain.Book>());
        }

        [Fact]
        public async Task Post_UpdateBookById_RepoError_Throws()
        {
            // Arrange
            long id = 10004;
            _mockedBookRepository
                .When(x => x.GetByIdAsync(id))
                .Do(x => 
                    { 
                        throw new CosmosException("Too many requests", HttpStatusCode.TooManyRequests, 429, "Tests", 0); 
                    });

            Kleber.Bookstore.Models.Book newBook = new Kleber.Bookstore.Models.Book()
            {
                Title = "Component Tests Bible",
                Author = "Comp",
                Price = 10
            };

            //Act - Assert
            await Assert.ThrowsAsync<CosmosException>(() => _client.SendAsync(System.Net.Http.HttpMethod.Put, $"/books/{id}", JsonConvert.SerializeObject(newBook)));
        }

    }
}
