using TekkenApp.Dtos;

namespace TekkenApp.Services
{
    public interface ICharactersService
    {
        public Task<bool> CreateCharacter(CharacterCreateDto obj);
        public Task<bool> DeleteCharacter(CharacterUpdateDto obj);
        public Task<CharacterReadDto> GetCharacterById(int id);
        public Task<IEnumerable<CharacterReadDto>> GetAllCharacters();
        public Task<bool> EditCharacter(CharacterUpdateDto obj);
    }
}
