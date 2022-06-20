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
using Microsoft.Identity.Client;
using System.Globalization;

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

        public async Task<string> AccessToken()
        {

            IConfidentialClientApplication app =
                ConfidentialClientApplicationBuilder.Create("ad27f8de-781a-4bac-81f3-12deeaba191e")
                .WithClientSecret("-IU8Q~vqsO.GPY3BSmsySzUjQ2D1a5v2UBOV1cHq")
                .WithAuthority(new Uri(String.Format(CultureInfo.InvariantCulture, "https://login.microsoftonline.com/{0}", "53dcff0f-b6b3-4434-8999-2dcd3eee506b")))
                .Build();

            string[] ResourceIds = new string[] { "api://939b9a23-2102-4fad-a971-8b5167a8619f/.default" };

            AuthenticationResult result = null;
            try
            {
                result = await app.AcquireTokenForClient(ResourceIds).ExecuteAsync();
            }
            catch (MsalClientException ex)
            {
                Console.WriteLine("-------> " + ex.Message);
                return "";
            }

            return result.AccessToken;
        }

        //
    }
}
