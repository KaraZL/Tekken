using Bogus;
using Bogus.Extensions;
using CharactersAPI.Models;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CharactersAPI.Tests.Integration.CharactersController
{
    [Collection("Characters API")]
    public class GetAllCharacterControllerTests
    {
        private readonly HttpClient _client;

        private readonly Faker<Character> _character = new Faker<Character>()
            .RuleFor(x => x.Name, faker => faker.Person.FirstName.ClampLength(1, 10))
            .RuleFor(x => x.Description, faker => faker.Lorem.Sentence(3))
            .RuleFor(x => x.Id, 1);

        public GetAllCharacterControllerTests(CharactersApiFactory factory)
        {
            _client = factory.CreateClient();
            var accessToken = factory.AccessToken().Result;
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", accessToken);

        }

        [Fact]
        public async Task GetAllCharacters_ReturnsCharacters_WhenCharactersExists()
        {
            //arrange
            var character = _character.Generate();
            var createdCharacterResponse = await _client.PostAsJsonAsync("api/Characters", character);

            //act
            var response = await _client.GetAsync("api/Characters");

            //assert
            var characterResponse = await response.Content.ReadFromJsonAsync<IEnumerable<Character>>();
            character.Id = characterResponse!.First().Id; 

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            characterResponse.Should().HaveCount(1);
            characterResponse!.Single().Should().BeEquivalentTo(character);

            //Cleanup
            await _client.DeleteAsync($"api/Characters/{characterResponse!.Single().Id}");
        }
    }
}
