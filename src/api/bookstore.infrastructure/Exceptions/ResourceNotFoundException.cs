using System;

namespace bookstore.Infrastructure.Exceptions
{
    public class ResourceNotFoundException : Exception
    {
        public ResourceNotFoundException(long id, string message) : base(message)
        {
            Id = id;
        }

        public ResourceNotFoundException(long id, string message, Exception innerException) : base(message, innerException)
        {
            Id = id;
        }

        public long Id { get; }
    }
}
