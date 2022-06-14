using AutoMapper;
using CharactersAPI.Dtos;
using CharactersAPI.Models;
using CharactersAPI.Repository;
using CharactersAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CharactersAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharactersController : ControllerBase
    {
        private readonly ICharacterService _service;
        private readonly IMapper _mapper;

        public CharactersController(ICharacterService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Character))]
        public async Task<IActionResult> GetCharacterById(int id)
        {
            if(id < 0)
            {
                return BadRequest();
            }

            var item = await _service.GetById(id);
            if (item == null)
                return BadRequest();

            return Ok(item);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Character>))]
        public async Task<IActionResult> GetAllCharacters()
        {
            var items = await _service.GetAllCharacters(); 
            return Ok(items);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCharacter(CharacterDto item)
        {
            if (item == null)
                return BadRequest();

            var character = _mapper.Map<Character>(item);

            var response = await _service.CreateCharacter(character);
            if (!response)
                return BadRequest();

            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> RemoveCharacter(int id)
        {
            if (id < 0)
                return BadRequest();

            var response = await _service.DeleteCharacter(id);
            if (!response)
                return BadRequest();

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCharacter(Character character)
        {
            if(character == null)
                return BadRequest();

            var exists = await _service.GetById(character.Id);
            if (exists is null)
                return BadRequest();

            var response = await _service.UpdateCharacter(character);
            if (!response)
                return BadRequest();

            return Ok();
        }
    }
}
