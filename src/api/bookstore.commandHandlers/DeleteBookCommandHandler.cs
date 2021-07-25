using bookstore.Integration;
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

        public async Task HandleAsync(long id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
