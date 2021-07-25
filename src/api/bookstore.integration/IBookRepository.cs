using bookstore.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace bookstore.Integration
{
    public interface IBookRepository
    {
        Task<Book> GetByIdAsync(long id);
        Task<IEnumerable<Book>> GetBooks(string sortBy, int pageSize, int pageNumber);
        Task SaveAsync(Book entity);
        Task DeleteAsync(long id);
    }
}
