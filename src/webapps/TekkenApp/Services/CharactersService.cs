using Microsoft.Identity.Client;
using System.Globalization;
using System.Net.Http.Headers;
using TekkenApp.Dtos;

namespace TekkenApp.Services
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

            var request = new HttpRequestMessage(HttpMethod.Get, "characters");
            var response = await _client.SendAsync(request, HttpCompletionOption.ResponseContentRead);
            if (!response.IsSuccessStatusCode)
                return Enumerable.Empty<CharacterReadDto>();

            var characters = await response.Content.ReadFromJsonAsync<IEnumerable<CharacterReadDto>>();

            return characters;
        }

        public async Task<CharacterReadDto> GetCharacterById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
