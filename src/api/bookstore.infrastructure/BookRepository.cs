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
using bookstore.Integration.Enums;

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

        public async Task<IEnumerable<Book>> GetBooks(SortBy sortBy)
        {
            try
            {
                List<Book> result = new List<Book>();
                string continuationToken = null;
                do
                {
                    var queryResponse = await QueryBooks(sortBy, continuationToken);
                    if (queryResponse.StatusCode != System.Net.HttpStatusCode.OK)
                        throw new OperationFailedException("Search operation failed");

                    continuationToken = queryResponse.ContinuationToken;
                    result.AddRange(queryResponse.Resource);
                }
                while (!string.IsNullOrEmpty(continuationToken));

                return result;
            }
            catch (CosmosException ex)
            {
                throw ex;
            }
        }

        private async Task<FeedResponse<Book>> QueryBooks(SortBy sortBy, string continuationToken = null)
        {
            var options = new QueryRequestOptions()
            {
                MaxItemCount = 20
            };

            var collectionQuery = _bookContainer.GetItemLinqQueryable<Book>(true, continuationToken, options);

            collectionQuery = OrderQuery(collectionQuery, sortBy);

            FeedIterator<Book> collectionIterator =
                    collectionQuery
                    .ToFeedIterator();

            var response = await collectionIterator.ReadNextAsync();

            return response;
        }

        private IOrderedQueryable<Book> OrderQuery(IOrderedQueryable<Book> query, SortBy sortBy)
        {
            return sortBy switch
            {
                SortBy.Author => query.OrderBy(o => o.Author),
                SortBy.Price => query.OrderBy(o => o.Price),
                _ => query.OrderBy(o => o.Title)
            };
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

