using bookstore.API.ComponentTests.Setup;
using bookstore.Integration;
using Microsoft.Azure.Cosmos;
using Newtonsoft.Json;
using NSubstitute;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace bookstore.API.ComponentTests
{
    public class CreateBookTests
    {
        private readonly IBookRepository _mockedBookRepository;
        private readonly ApiClientFixture _client;
        public CreateBookTests()
        {
            _mockedBookRepository = Substitute.For<IBookRepository>();
            _client = new ApiClientFixture(_mockedBookRepository);
        }

        [Fact]
        public async Task Post_CreateBook_ReturnsObjectResult_WithId()
        {
            // Arrange
            Kleber.Bookstore.Models.Book newBook = new Kleber.Bookstore.Models.Book()
            {
                Id = 9999, 
                Title = "Component Test Bible", 
                Author = "Comp", 
                Price = 10
            };

            //Act
            var defaultPage = await _client.SendAsync(System.Net.Http.HttpMethod.Post, "/books", JsonConvert.SerializeObject(newBook));
            
            // Assert
            Assert.Equal(HttpStatusCode.Created, defaultPage.StatusCode);
            var content = await defaultPage.Content.ReadAsStringAsync();
            Assert.NotNull(content);

            await _mockedBookRepository.Received(1).SaveAsync(Arg.Any<Domain.Book>());
        }

        [Fact]
        public async Task Post_CreateBook_EmptyTitle_ReturnsBadRequest()
        {
            // Arrange
            Kleber.Bookstore.Models.Book newBook = new Kleber.Bookstore.Models.Book()
            {
                Id = 9999,
                Title = "",
                Author = "Comp",
                Price = 10
            };

            //Act
            var defaultPage = await _client.SendAsync(System.Net.Http.HttpMethod.Post, "/books", JsonConvert.SerializeObject(newBook));

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, defaultPage.StatusCode);
            await _mockedBookRepository.Received(0).SaveAsync(Arg.Any<Domain.Book>());
        }

        [Fact]
        public async Task Post_CreateBook_RepoError_Throws()
        {
            // Arrange
            _mockedBookRepository
                .When(x => x.SaveAsync(Arg.Any<Domain.Book>()))
                .Do(x => 
                    { 
                        throw new CosmosException("Too many requests", HttpStatusCode.TooManyRequests, 429, "Tests", 0); 
                    });

            Kleber.Bookstore.Models.Book newBook = new Kleber.Bookstore.Models.Book()
            {
                Id = 9999,
                Title = "Component Tests Bible",
                Author = "Comp",
                Price = 10
            };

            //Act - Assert
            await Assert.ThrowsAsync<CosmosException>(() => _client.SendAsync(System.Net.Http.HttpMethod.Post, "/books", JsonConvert.SerializeObject(newBook)));
        }

    }
}
