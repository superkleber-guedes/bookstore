using bookstore.Infrastructure.Config;
using System;
using System.Threading.Tasks;
using Xunit;
using Microsoft.Extensions.Options;
using bookstore.Domain;
using Microsoft.Extensions.Configuration;
using bookstore.Infrastructure.Exceptions;
using Microsoft.Azure.Cosmos;
using System.Linq;

namespace bookstore.Infrastructure.IntegrationTests
{

    [Trait("TestType", "IntegrationTests")]
    public class BookRepositoryTests
    {
        private readonly BookRepository _bookRepository;
        private bool _createDatabaseAndContainer;
        private readonly CosmosDbConfiguration _cosmosConfig;
        public BookRepositoryTests()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json").Build();
            
            _cosmosConfig = config.GetSection("CosmosDb").Get<CosmosDbConfiguration>();
            var repositoryConfiguration = Options.Create(_cosmosConfig);

            _bookRepository = new BookRepository(repositoryConfiguration);

            //extra - this is not part of a normal integration tests, but I am reusing the tests to create database and data
            _createDatabaseAndContainer = config.GetSection("CreateDatabaseAndContainer")?.Get<bool>() ?? false;
        }

        /// <summary>
        /// Help code to create database and container by running Integration Tests
        /// This is just a shortcut to get database and container created for other people running this code
        /// </summary>
        /// <returns></returns>
        private async Task CreateDatabaseAndContainerOnFirstRun()
        {
            if (_createDatabaseAndContainer)
            {
                CosmosClient client = new CosmosClient(_cosmosConfig.ConnectionString);
                var response = await client.CreateDatabaseIfNotExistsAsync(_cosmosConfig.DatabaseName);
                Assert.True(response.StatusCode == System.Net.HttpStatusCode.OK || response.StatusCode == System.Net.HttpStatusCode.Created);
                Database database = client.GetDatabase(_cosmosConfig.DatabaseName);

                var containerProperties = new ContainerProperties()
                {
                    Id = _cosmosConfig.ContainerName,
                    PartitionKeyPath = "/id"
                };
                
                var containerResponse = await database.CreateContainerIfNotExistsAsync(containerProperties);
                Assert.True(containerResponse.StatusCode == System.Net.HttpStatusCode.OK || containerResponse.StatusCode == System.Net.HttpStatusCode.Created);
            }
        }

        [Theory]
        [InlineData(1, "Winnie-the-Pooh", "A. A. Milne", 19.25)]
        [InlineData(2, "Pride and Prejudice", "Jane Austen", 5.49)]
        [InlineData(3, "Romeo and Juliet", "William Shakespeare", 6.95)]
        public async Task FeedDatabaseTest(long id, string title, string author, decimal price)
        {
            await CreateDatabaseAndContainerOnFirstRun();

            Book _newBook = new Book(id, title, author, price);

            await _bookRepository.SaveAsync(_newBook);
            var result = await _bookRepository.GetByIdAsync(id);

            //Assert the values returned from DB matches the values sent
            Assert.Equal(result.Id, _newBook.Id);
            Assert.Equal(result.BookId, _newBook.BookId);
            Assert.Equal(result.Title, _newBook.Title);
            Assert.Equal(result.Author, _newBook.Author);
            Assert.Equal(result.Price, _newBook.Price);
        }

        [Fact]
        public async Task DeleteTest()
        {
            Book newBook = new Book(999, "Not a worthy book", "Not a worthy author", 1.50M);

            await _bookRepository.SaveAsync(newBook);
            var result = await _bookRepository.GetByIdAsync(newBook.BookId);
            Assert.NotNull(result);

            await _bookRepository.DeleteAsync(newBook.BookId);

            await Assert.ThrowsAsync<ResourceNotFoundException>(() => _bookRepository.GetByIdAsync(newBook.BookId));
        }

        /// <summary>
        /// FeedDatabaseTest must run before this test, it runs before because xunit orders them alphabetically when all tests run
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetBooksByAuthor()
        {
            var response = (await _bookRepository.GetBooks(Integration.Enums.SortBy.Author)).ToList();

            Assert.True(response.Count > 0);

            var responseList = response.ToList();
            var reorderedList = response.OrderBy(o => o.Author).ToList();

            for (int i = 0; i < response.Count; i++)
            {
                Assert.Equal(response[0].Author, reorderedList[0].Author);
            }
        }

        [Fact]
        public async Task GetBooksByPrice()
        {
            var response = (await _bookRepository.GetBooks(Integration.Enums.SortBy.Price)).ToList();

            Assert.True(response.Count > 0);

            var responseList = response.ToList();
            var reorderedList = response.OrderBy(o => o.Price).ToList();

            for (int i = 0; i < response.Count; i++)
            {
                Assert.Equal(response[0].Price, reorderedList[0].Price);
            }
        }
    }
}
