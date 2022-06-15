using AutoMapper;
using CharactersAPI.Controllers;
using CharactersAPI.Dtos;
using CharactersAPI.Models;
using CharactersAPI.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace CharactersAPI.Tests.Unit
{
    public class CharactersControllerTests
    {
        private readonly CharactersController _sut;

        private readonly IMapper _mapper = Substitute.For<IMapper>();
        private readonly ICharacterService _service = Substitute.For<ICharacterService>();

        Character character = new Character
        {
            Id = 1,
            Name = "Zafina",
            Description = "Assassin"
        };

        Character invalidCharacter = new Character
        {
            Id = 1,
            Name = "Zafinaaaaaaaaaaaaaaaaaaaa",
            Description = "Assassin"
        };

        public CharactersControllerTests()
        {
            _sut = new CharactersController(_service, _mapper);
        }

        [Fact]
        public async Task CreateCharacter_ReturnsOK_WhenCharacterDtoIsValid()
        {
            //Arrange

            CharacterDto characterDto = new CharacterDto
            {
                Name = "Zafina",
                Description = "Assassin"
            };

            _service.CreateCharacter(character).Returns(true);
            _mapper.Map<Character>(characterDto).Returns(character);

            //Act
            var response = (OkResult) await _sut.CreateCharacter(characterDto);

            //Assert
            response.StatusCode.Should().Be(StatusCodes.Status200OK);
        }

        [Fact]
        public async Task CreateCharacter_ReturnsBadRequest_WhenCharacterDtoInvalid()
        {
            //Arrange
            CharacterDto characterDto = new CharacterDto
            {
                Name = "Zafinaaaaaaaaaaaaaaaaaaaa",
                Description = "Assassin"
            };

            _service.CreateCharacter(invalidCharacter).Returns(false);

            //Act
            var response = (BadRequestResult) await _sut.CreateCharacter(characterDto);

            //Assert
            response.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        }

        [Fact]
        public async Task GetCharacterById_ReturnsCharacter_WhenIdValid()
        {
            //Arrange
            int id = 1;
            
            _service.GetById(id).Returns(character);

            //Act
            var response = (OkObjectResult) await _sut.GetCharacterById(id);
            
            //Assert
            response.StatusCode.Should().Be(StatusCodes.Status200OK);
            response.Value.Should().BeEquivalentTo(character);
        }

        [Fact]
        public async Task GetCharacterById_ReturnsBadRequest_WhenIdInvalid()
        {
            //Arrange
            _service.GetById(Arg.Any<int>()).ReturnsNull();

            //Act
            var response = (BadRequestResult) await _sut.GetCharacterById(Arg.Any<int>());

            //Assert
            response.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        }

        [Fact]
        public async Task GetAllCharacters_ReturnsCharacters_WhenCharactersExists()
        {

            //Arrange
            IEnumerable<Character> characters = new List<Character>();
            _service.GetAllCharacters().Returns(characters);

            //Act
            var response = (OkObjectResult) await _sut.GetAllCharacters();

            //Assert
            response.StatusCode.Should().Be(StatusCodes.Status200OK);
            response.Value.Should().BeEquivalentTo(characters);
        }

        [Fact]
        public async Task UpdateCharacter_ReturnsOk_WhenCharacterExists()
        {
            //Arrange
            _service.GetById(character.Id).Returns(character);
            _service.UpdateCharacter(character).Returns(true);

            //Act
            var response = (OkResult) await _sut.UpdateCharacter(character);

            //Assert
            response.StatusCode.Should().Be(StatusCodes.Status200OK);
        }

        [Fact]
        public async Task RemoveCharacter_ReturnsOk_WhenCharacterExists()
        {
            //Arrange
            _service.DeleteCharacter(character.Id).Returns(true);

            //Act
            var response = (OkResult) await _sut.RemoveCharacter(character.Id);

            //Assert
            response.StatusCode.Should().Be(StatusCodes.Status200OK);

        }


    }
}
