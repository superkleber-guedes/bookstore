using bookstore.Integration;
using bookstore.QueryHandlers.Models;
using bookstore.QueryHandlers.Queries;
using System;
using System.Collections.Generic;
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

        public Task<IEnumerable<Book>> GetBooks(GetBooksQuery query)
        {
            throw new NotImplementedException();
        }
    }
}
