using bookstore.Domain;
using bookstore.Integration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Options;
using bookstore.Infrastructure.Config;

namespace bookstore.Infrastructure
{
    public class BookRepository : IBookRepository
    {
        private readonly Container _bookContainer;
        public BookRepository(IOptions<CosmosDbConfiguration> cosmosDbConfiguration)
        {
            var cosmosClient = new CosmosClient($"{cosmosDbConfiguration.Value.EndpointUri}/{cosmosDbConfiguration.Value.ConnectionString}");
            var database = cosmosClient.GetDatabase(cosmosDbConfiguration.Value.DatabaseName);
            _bookContainer = database.GetContainer(cosmosDbConfiguration.Value.ContainerName);
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Book>> GetBooks(string sortBy)
        {
            throw new NotImplementedException();
        }

        public Task<Book> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task SaveAsync(Book entity)
        {
            throw new NotImplementedException();
        }
    }
}
