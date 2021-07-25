using Newtonsoft.Json;

namespace bookstore.Domain
{
    public class Book
    {
        [JsonConstructor]
        public Book(long id, string title, string author, decimal price)
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
    }
}
