using CharactersAPI.Data;
using CharactersAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CharactersAPI.Repository
{
    public class CharacterSqlRepository : ISqlRepository<Character>
    {
        private readonly DatabaseContext _context;
        public CharacterSqlRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<bool> Add(Character item)
        {
            await _context.AddAsync(item);
            return await SaveChanges();
        }

        public async Task<bool> Delete(int id)
        {
            var character = await _context.Character.FindAsync(id);
            _context.Character.Remove(character);
            return await SaveChanges();
        }

        public async Task<Character> Get(int id)
        {
            var character = await _context.Character.FindAsync(id);
            return character;
        }

        public async Task<IEnumerable<Character>> GetAll()
        {
            var items = from p in _context.Character select p;
            return await items.ToListAsync();

        }

        public async Task<bool> Update(Character item)
        {
            _context.Update(item);
            return await SaveChanges();
        }

        private async Task<bool> SaveChanges()
        {
            var response = await _context.SaveChangesAsync();
            return response > 0;
        }
    }
}
