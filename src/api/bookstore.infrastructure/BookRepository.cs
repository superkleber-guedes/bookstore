using bookstore.Domain;
using bookstore.Integration;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Options;
using bookstore.Infrastructure.Config;
using bookstore.Infrastructure.Exceptions;
using System.Linq;
using Microsoft.Azure.Cosmos.Linq;

namespace bookstore.Infrastructure
{
    public class BookRepository : IBookRepository
    {
        private readonly Container _bookContainer;
        public BookRepository(IOptions<CosmosDbConfiguration> cosmosDbConfiguration)
        {
            var cosmosClient = new CosmosClient(cosmosDbConfiguration.Value.ConnectionString);
            var database = cosmosClient.GetDatabase(cosmosDbConfiguration.Value.DatabaseName);
            _bookContainer = database.GetContainer(cosmosDbConfiguration.Value.ContainerName);
        }

        public async Task DeleteAsync(long id)
        {
            try
            {
                var response = await _bookContainer.DeleteItemAsync<Book>(id.ToString(), new PartitionKey(id.ToString()));
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                throw new ResourceNotFoundException(id, "Book not found", ex);
            }
            catch (CosmosException ex)
            {
                //I have not added logging, but that's a good place to do it
                throw ex;
            }
        }

        public async Task<IEnumerable<Book>> GetBooks(string sortBy, int pageSize, int pageNumber)
        {
            try
            {
                if (pageSize <= 0) throw new OperationFailedException("Page size must be greater than 0");
                if (pageNumber <= 0) throw new OperationFailedException("Page number must be greater than 0");

                var options = new QueryRequestOptions()
                {
                    MaxItemCount = pageSize
                };

                var collectionQuery = _bookContainer.GetItemLinqQueryable<Book>(true, null, options);

                //this should be done somewhere else
                switch (sortBy.ToLower().Trim())
                {
                    case "author":
                        {
                            collectionQuery = collectionQuery.OrderBy(o => o.Author);
                            break;
                        }
                    case "title":
                        {
                            collectionQuery = collectionQuery.OrderBy(o => o.Title);
                            break;
                        }
                    case "price":
                        {
                            collectionQuery = collectionQuery.OrderBy(o => o.Price);
                            break;
                        }
                    default:
                        {
                            collectionQuery = collectionQuery.OrderBy(o => o.BookId);
                            break;
                        }
                }
                
                FeedIterator<Book> queryResultSetIterator =
                    collectionQuery
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToFeedIterator();

                List<Book> result = new List<Book>();

                while (queryResultSetIterator.HasMoreResults)
                {
                    var response = await queryResultSetIterator.ReadNextAsync();

                    if (response.StatusCode != System.Net.HttpStatusCode.OK) throw new OperationFailedException("Search operation failed");

                    result.AddRange(response.Resource);
                }

                return result;
            }
            catch (CosmosException ex)
            {
                throw ex;
            }
        }

        public async Task<Book> GetByIdAsync(long id)
        {
            try
            {
                var response = await _bookContainer.ReadItemAsync<Book>(id.ToString(), new PartitionKey(id.ToString()), new ItemRequestOptions());
                return response;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                throw new ResourceNotFoundException(id, "Book not found", ex);
            }
            catch (CosmosException ex)
            {
                //I have not added logging, but that's a good place to do it
                throw ex;
            }
        }

        public async Task SaveAsync(Book entity)
        {
            try
            {
                //I could have added eTag validation here and partition Key, but keeping it simple
                var response = await _bookContainer.UpsertItemAsync<Book>(entity, new PartitionKey(entity.Id), new ItemRequestOptions() { });

                var isSuccessfull = response?.StatusCode == System.Net.HttpStatusCode.OK ||
                                    response?.StatusCode == System.Net.HttpStatusCode.Created;

                if (!isSuccessfull) throw new OperationFailedException("Operation has failed");
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                throw new ResourceNotFoundException(entity.BookId, "Book not found", ex);
            }
            catch (CosmosException ex)
            {
                //I have not added logging, but that's a good place to do it
                throw ex;
            }
        }
    }
}

