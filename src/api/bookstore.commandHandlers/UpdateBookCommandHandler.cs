using bookstore.CommandHandlers.Commands;
using bookstore.Integration;
using System;
using System.Threading.Tasks;

namespace bookstore.CommandHandlers
{
    public class UpdateBookCommandHandler : IUpdateBookCommandHandler
    {
        private readonly IBookRepository _repository;

        public UpdateBookCommandHandler(IBookRepository repository)
        {
            _repository = repository;
        }

        public Task HandleAsync(UpdateBookCommand command)
        {
            throw new NotImplementedException();
        }
    }
}
