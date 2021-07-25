namespace bookstore.QueryHandlers.Models
{
    public class Book
    {
        private Book(long id, string title, string author, decimal price)
        {
            Id = id;
            Title = title;
            Author = author;
            Price = price;
        }

        public long Id { get; }
        public string Title { get; }
        public string Author { get; }
        public decimal Price { get; }

        public Book FromDomain(Domain.Book book)
        {
            return new Book(book.Id, book.Title, book.Author, book.Price);
        }
    }
}
