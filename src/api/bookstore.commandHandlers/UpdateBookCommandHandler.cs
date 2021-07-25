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

        public async Task HandleAsync(UpdateBookCommand command)
        {
            Domain.Book updateBook = new Domain.Book(
                command.Id,
                command.Title,
                command.Author,
                command.Price);

            await _repository.SaveAsync(updateBook);
        }
    }
}
