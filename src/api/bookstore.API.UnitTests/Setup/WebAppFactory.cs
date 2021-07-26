using bookstore.Infrastructure.Config;
using bookstore.Integration;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using System;
using System.Linq;

namespace bookstore.API.ComponentTests.Setup
{
    public class WebAppFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        private readonly IBookRepository _mockedBookRepository;

        public WebAppFactory(IBookRepository mockedBookRepository)
        {
            _mockedBookRepository = mockedBookRepository;
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(IBookRepository));
                services.Remove(descriptor);

                services.AddTransient(s => _mockedBookRepository);

                var sp = services.BuildServiceProvider();
            });
        }
    }
}
