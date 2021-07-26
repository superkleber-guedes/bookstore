using bookstore.Integration;
using bookstore.QueryHandlers.Models;
using bookstore.QueryHandlers.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bookstore.QueryHandlers
{
    public class GetBooksQueryHandler : IGetBooksQueryHandler
    {
        private readonly IBookRepository _repository;

        public GetBooksQueryHandler(IBookRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Book>> HandleAsync(GetBooksQuery query)
        {
            var queryResponse = await _repository.GetBooks(query.SortBy);

            return queryResponse.Select(Book.FromDomain);
        }
    }
}
