using CharactersAPI.Dtos;
using CharactersAPI.Models;
using CharactersAPI.Repository;

namespace CharactersAPI.Services
{
    public class CharacterService : ICharacterService
    {
        private readonly ISqlRepository<Character> _repo;

        public CharacterService(ISqlRepository<Character> repo)
        {
            _repo = repo;
        }

        public async Task<bool> CreateCharacter(Character character)
        {
            if (character == null)
                return false;

            var response = await _repo.Add(character);

            return response;
        }

        public async Task<bool> DeleteCharacter(int id)
        {
            if (id < 0)
                return false;

            var response = await _repo.Delete(id);

            return response;
        }

        public async Task<IEnumerable<Character>> GetAllCharacters()
        {
            return await _repo.GetAll();
        }

        public async Task<Character?> GetById(int id)
        {
            if (id < 0)
                return null;

            var character = await _repo.Get(id);
            if (character == null)
                return null;

            return character;
        }

        public async Task<bool> UpdateCharacter(Character character)
        {
            if (character.Id < 0)
                return false;

            var response = await _repo.Update(character);
            if (!response)
                return false;

            return true;
        }
    }
}
