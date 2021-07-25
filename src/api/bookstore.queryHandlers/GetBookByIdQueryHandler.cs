using bookstore.Integration;
using bookstore.QueryHandlers.Models;
using System;
using System.Threading.Tasks;

namespace bookstore.QueryHandlers
{
    public class GetBookByIdQueryHandler : IGetBookByIdQueryHandler
    {
        private readonly IBookRepository _repository;

        public GetBookByIdQueryHandler(IBookRepository repository)
        {
            _repository = repository;
        }

        public Task<Book> GetBookById(long id)
        {
            throw new NotImplementedException();
        }
    }
}
