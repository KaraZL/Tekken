using CharactersAPI.Dtos;
using CharactersAPI.Models;

namespace CharactersAPI.Services
{
    public interface ICharacterService
    {
        public Task<bool> CreateCharacter(Character character);
        public Task<IEnumerable<Character>> GetAllCharacters();
        public Task<Character> GetById(int id);

        public Task<bool> DeleteCharacter(int id);

        public Task<bool> UpdateCharacter(Character character);
    }
}
