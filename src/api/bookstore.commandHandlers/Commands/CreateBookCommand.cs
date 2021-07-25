namespace bookstore.CommandHandlers.Commands
{
    public class CreateBookCommand
    {
        public CreateBookCommand(long id, string title, string author, decimal price)
        {
            Id = id;
            Title = title;
            Author = author;
            Price = price;

            ValidateCommand();
        }


        public long Id { get; }
        public string Title { get; }
        public string Author { get; }
        public decimal Price { get; }

        private void ValidateCommand()
        {
            //I could have created custom Exceptions, but because of time constraints, decided to go for System Exceptions
            if (Id < 0) throw new System.ArgumentException("'Id' must not be negative.", nameof(Id));
            if (string.IsNullOrWhiteSpace(Title)) throw new System.ArgumentException("'Title' must not be null, empty or white space.", nameof(Title));
            if (string.IsNullOrWhiteSpace(Author)) throw new System.ArgumentException("'Author' must not be null, empty or white space.", nameof(Author));
            if (Price < 0) throw new System.ArgumentException("'Price' must not be negative.", nameof(Price));
        }
    }
}
