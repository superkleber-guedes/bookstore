using bookstore.CommandHandlers;
using bookstore.Infrastructure;
using bookstore.Infrastructure.Config;
using bookstore.Integration;
using bookstore.QueryHandlers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Kleber.Bookstore
{
    internal class DependencyRegistration
    {

        /// <summary>
        /// Register static services that does not change between environment or contexts
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureStaticDependencies(IServiceCollection services)
        {
            AddCommandHandlers(services);
            AddQueryHandlers(services);
        }

        /// <summary>
        /// Register dynamic services that changes between environments or context
        /// </summary>
        /// <param name="context"></param>
        /// <param name="services"></param>
        public static void ConfigureProductionDependencies(WebHostBuilderContext context, IServiceCollection services)
        {
            services.Configure<CosmosDbConfiguration>(context.Configuration.GetSection("CosmosDB"));
            services.AddTransient<IBookRepository, BookRepository>();
        }

        private static void AddCommandHandlers(IServiceCollection services)
        {
            services.AddTransient<ICreateBookCommandHandler, CreateBookCommandHandler>();
            services.AddTransient<IUpdateBookCommandHandler, UpdateBookCommandHandler>();
            services.AddTransient<IDeleteBookCommandHandler, DeleteBookCommandHandler>();
        }

        private static void AddQueryHandlers(IServiceCollection services)
        {
            services.AddTransient<IGetBookByIdQueryHandler, GetBookByIdQueryHandler>();
            services.AddTransient<IGetBooksQueryHandler, GetBooksQueryHandler>();
        }
    }
}