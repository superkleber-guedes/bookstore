using bookstore.Domain;
using bookstore.Integration.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace bookstore.Integration
{
    public interface IBookRepository
    {
        Task<Book> GetByIdAsync(long id);
        Task<IEnumerable<Book>> GetBooks(SortBy sortBy);
        Task SaveAsync(Book entity);
        Task DeleteAsync(long id);
    }
}
