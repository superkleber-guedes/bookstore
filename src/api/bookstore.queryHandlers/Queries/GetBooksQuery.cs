namespace bookstore.QueryHandlers.Queries
{
    public class GetBooksQuery
    {
        public GetBooksQuery(string sortBy)
        {
            SortBy = sortBy;
            
            ValidateQuery();
        }

        public string SortBy { get; }

        private void ValidateQuery()
        {
            //I could have created custom Exceptions, but because of time constraints, decided to go for System Exceptions
            //Validation could have been made better on SortBy as you have limited sorting options
            if (string.IsNullOrWhiteSpace(SortBy)) throw new System.ArgumentException("'SortBy' is not valid", nameof(SortBy));
        }
    }
}
