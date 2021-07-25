using bookstore.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace bookstore.Integration
{
    public interface IBookRepository
    {
        Task<Book> GetByIdAsync(Guid id);
        Task<IEnumerable<Book>> GetBooks(string sortBy);
        Task SaveAsync(Book entity);
        Task DeleteAsync(Guid id);
    }
}
