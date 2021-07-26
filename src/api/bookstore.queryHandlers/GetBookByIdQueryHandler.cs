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

        public async Task<Book> HandleAsync(long id)
        {
            var result = await _repository.GetByIdAsync(id);
            return Book.FromDomain(result);
        }
    }
}
