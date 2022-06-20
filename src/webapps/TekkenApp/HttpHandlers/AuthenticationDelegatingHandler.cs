using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System.Globalization;
using System.Net.Http.Headers;

namespace TekkenApp.HttpHandlers
{
    public class AuthenticationDelegatingHandler : DelegatingHandler
    {
        private readonly HttpClient _client;
        private readonly IConfiguration _config;

        public AuthenticationDelegatingHandler(HttpClient client, IConfiguration config)
        {
            _client = client;
            _config = config;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            IConfidentialClientApplication app =
                ConfidentialClientApplicationBuilder.Create(_config["AzureAD:ClientId"])
                .WithClientSecret(_config["AzureAD:ClientSecret"])
                .WithAuthority(new Uri(String.Format(CultureInfo.InvariantCulture, _config["AzureAD:Instance"], _config["AzureAD:TenantId"])))
                .Build();

            string[] ResourceIds = new string[] { _config["AzureAD:ResourceId"] };

            AuthenticationResult result = null;
            try
            {
                result = await app.AcquireTokenForClient(ResourceIds).ExecuteAsync();
            }
            catch (MsalClientException ex)
            {
                Console.WriteLine("-------> " + ex.Message);
            }

            var accessToken = result.AccessToken;

            if (!string.IsNullOrEmpty(accessToken))
                request.Headers.Authorization = new AuthenticationHeaderValue("bearer", accessToken);

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
