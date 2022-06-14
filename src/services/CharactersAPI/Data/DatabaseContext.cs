using CharactersAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CharactersAPI.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }

        public DbSet<Character> Character { get; set; }
    }
}
