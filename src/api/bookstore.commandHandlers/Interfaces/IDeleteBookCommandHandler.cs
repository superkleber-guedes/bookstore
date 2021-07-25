using System.Threading.Tasks;

namespace bookstore.CommandHandlers
{
    public interface IDeleteBookCommandHandler
    {
        //I could have used IMediator, or even create my own mediator, but decided for simplicity
        public Task HandleAsync(long id);
    }

}
