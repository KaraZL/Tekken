using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using CharactersAPI.Data;
using DotNet.Testcontainers.Containers;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Configurations;

namespace CharactersAPI.Tests.Integration
{
    public class CharactersApiFactory : WebApplicationFactory<IApiMakerIntegration>, IAsyncLifetime
    {
        private readonly TestcontainerDatabase _container = new TestcontainersBuilder<MsSqlTestcontainer>()
            .WithDatabase(new MsSqlTestcontainerConfiguration
            {
                Password = "Password?!1",
                Port = 1433
            })
            .Build();

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            
            builder.ConfigureLogging(logging =>
            {
                logging.ClearProviders(); //Elastic stack and other stuffs related to logging
            });

            builder.ConfigureTestServices(services =>
            {
                services.RemoveAll(typeof(IHostedService)); //Background services
                services.RemoveAll(typeof(DatabaseContext)); //Remove current EF

                services.AddDbContext<DatabaseContext>(options =>
                {
                    options.UseSqlServer(_container.ConnectionString);
                });
            });

        }

        public async Task InitializeAsync()
        {
            await _container.StartAsync();
        }

        public new async Task DisposeAsync()
        {
            await _container.StopAsync();
        }
    }
}
