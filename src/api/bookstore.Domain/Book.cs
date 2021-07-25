using Newtonsoft.Json;

namespace bookstore.Domain
{
    public class Book
    {
        [JsonConstructor]
        public Book(long id, string title, string author, decimal price)
        {
            BookId = id;
            Title = title;
            Author = author;
            Price = price;
        }

        [JsonProperty("id")]
        public string Id { get { return BookId.ToString(); } }
        [JsonProperty("BookId")]
        public long BookId { get; }
        public string Title { get; }
        public string Author { get; }
        public decimal Price { get; }
    }
}
