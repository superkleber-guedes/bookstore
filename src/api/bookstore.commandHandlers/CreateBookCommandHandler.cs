using bookstore.CommandHandlers.Commands;
using bookstore.Integration;
using System;
using System.Threading.Tasks;

namespace bookstore.CommandHandlers
{
    public class CreateBookCommandHandler : ICreateBookCommandHandler
    {
        private readonly IBookRepository _repository;

        public CreateBookCommandHandler(IBookRepository repository)
        {
            _repository = repository;
        }

        public Task<long> HandleAsync(CreateBookCommand command)
        {
            throw new NotImplementedException();
        }
    }
}
