using bookstore.CommandHandlers.Commands;
using System.Threading.Tasks;

namespace bookstore.CommandHandlers
{
    public interface ICreateBookCommandHandler
    {
        //I could have used IMediator, or even create my own mediator, but decided for simplicity
        public Task<long> HandleAsync(CreateBookCommand command);
    }
}
