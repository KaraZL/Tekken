using TekkenAppAssembly.Dtos;
using Microsoft.Identity.Client;
using System.Globalization;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace TekkenAppAssembly.Services
{
    public class CharactersService : ICharactersService
    {
        private readonly HttpClient _client;
        private readonly IConfiguration _config;

        public CharactersService(HttpClient client, IConfiguration config)
        {
            _client = client;
            _config = config;
        }

        public async Task<AuthenticationResult> RunAsync()
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

            return result;

            //var accessToken = result.AccessToken;
        }

        public Task<bool> CreateCharacter(CharacterCreateDto obj)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteCharacter(CharacterUpdateDto obj)
        {
            throw new NotImplementedException();
        }

        public Task<bool> EditCharacter(CharacterUpdateDto obj)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<CharacterReadDto>> GetAllCharacters()
        {

            var result = await RunAsync();

            var accessToken = result.AccessToken;

            var headers = _client.DefaultRequestHeaders;
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", accessToken);

            var response = await _client.GetAsync("characters");
            var characters = await response.Content.ReadFromJsonAsync<IEnumerable<CharacterReadDto>>();

            return characters;


        }

        public Task<CharacterReadDto> GetCharacterById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
