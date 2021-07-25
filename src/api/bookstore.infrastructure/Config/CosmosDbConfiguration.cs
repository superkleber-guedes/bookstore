namespace bookstore.Infrastructure.Config
{
    public class CosmosDbConfiguration
    {
        public string EndpointUri { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string ContainerName { get; set; }
    }
}
