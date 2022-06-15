using Bogus;
using Bogus.Extensions;
using CharactersAPI.Dtos;
using CharactersAPI.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CharactersAPI.Tests.Integration.CharactersController
{
    [Collection("Characters API")]
    public class CreateCharacterControllerTests
    {
        private readonly HttpClient _client;

        private readonly Faker<Character> _character = new Faker<Character>()
            .RuleFor(x => x.Name, faker => faker.Person.FirstName.ClampLength(1, 10))
            .RuleFor(x => x.Description, faker => faker.Lorem.Sentence(3))
            .RuleFor(x => x.Id, 1);

        private readonly Faker<CharacterDto> _characterDto = new Faker<CharacterDto>()
            .RuleFor(x => x.Name, faker => faker.Person.FirstName.ClampLength(1, 10))
            .RuleFor(x => x.Description, faker => faker.Lorem.Sentence(3));

        public CreateCharacterControllerTests(CharactersApiFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task CreateCharacter_ReturnsOK_WhenCharacterDtoIsValid()
        {
            //Arrange
            var characterDto = _characterDto.Generate();

            //Act
            var response = await _client.PostAsJsonAsync("api/Characters", characterDto);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            //Cleaup
            var getResponse = await _client.GetAsync("api/Characters");
            var charactersResponse = await getResponse.Content.ReadFromJsonAsync<IEnumerable<Character>>();
            foreach(var item in charactersResponse)
            {
                await _client.DeleteAsync($"api/Characters/{item.Id}");
            }
        }

        [Fact]
        public async Task CreateCharacter_ReturnsBadRequest_WhenCharacterDtoInvalid()
        {
            //Arrange
            var invalidName = "zafinaaaaaaaaaaaaaaaaaa"; //must not be 10+
            var invalidDto = _characterDto.Clone()
                .RuleFor(x => x.Name, invalidName)
                .Generate();

            //Act
            var response = await _client.PostAsJsonAsync("api/Characters", invalidDto);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            //Cleaup
            var getResponse = await _client.GetAsync("api/Characters");
            var charactersResponse = await getResponse.Content.ReadFromJsonAsync<IEnumerable<Character>>();
            foreach (var item in charactersResponse)
            {
                await _client.DeleteAsync($"api/Characters/{item.Id}");
            }
        }

    }
}
