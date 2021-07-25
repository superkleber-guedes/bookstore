using bookstore.QueryHandlers.Models;
using System.Threading.Tasks;

namespace bookstore.QueryHandlers
{
    public interface IGetBookByIdQueryHandler
    {
        //I could have used IMediator, or even create my own mediator, but decided for simplicity
        public Task<Book> GetBookById(long id);
    }
}
