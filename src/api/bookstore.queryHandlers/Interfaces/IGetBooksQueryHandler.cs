using bookstore.QueryHandlers.Models;
using bookstore.QueryHandlers.Queries;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace bookstore.QueryHandlers
{
    public interface IGetBooksQueryHandler
    {
        //I could have used IMediator, or even create my own mediator, but decided for simplicity
        public Task<IEnumerable<Book>> HandleAsync(GetBooksQuery query);
    }
}
