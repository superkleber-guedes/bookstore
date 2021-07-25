using bookstore.Integration;
using System;
using System.Threading.Tasks;

namespace bookstore.CommandHandlers
{
    public class DeleteBookCommandHandler : IDeleteBookCommandHandler
    {
        private readonly IBookRepository _repository;

        public DeleteBookCommandHandler(IBookRepository repository)
        {
            _repository = repository;
        }

        public Task HandleAsync(long id)
        {
            throw new NotImplementedException();
        }
    }
}
