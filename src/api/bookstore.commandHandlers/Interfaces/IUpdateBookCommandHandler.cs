using bookstore.CommandHandlers.Commands;
using System.Threading.Tasks;

namespace bookstore.CommandHandlers
{
    public interface IUpdateBookCommandHandler
    {
        //I could have used IMediator, or even create my own mediator, but decided for simplicity
        public Task HandleAsync(UpdateBookCommand command);
    }

}
