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

        public async Task<long> HandleAsync(CreateBookCommand command)
        {
            Domain.Book newBook = new Domain.Book(
                command.Id,
                command.Title,
                command.Author,
                command.Price);

            await _repository.SaveAsync(newBook);

            return newBook.BookId;
        }
    }
}
