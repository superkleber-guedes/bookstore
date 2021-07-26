using bookstore.Integration.Enums;

namespace bookstore.QueryHandlers.Queries
{
    public class GetBooksQuery
    {
        public GetBooksQuery(string sortBy)
        {
            SortBy = (sortBy?.ToLower()?.Trim() ?? "title") switch
            {
                "author" => SortBy.Author,
                "price" => SortBy.Price,
                "title" => SortBy.Title,
                _ => throw new System.ArgumentOutOfRangeException("SortBy is not valid")
            };
        }

        public SortBy SortBy { get; }
    }
}
