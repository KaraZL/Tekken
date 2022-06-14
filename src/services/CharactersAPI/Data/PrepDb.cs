using CharactersAPI.Models;
using CharactersAPI.Policies;
using Microsoft.EntityFrameworkCore;

namespace CharactersAPI.Data
{
    public class PrepDb
    {
        public static void PrepPopulation(WebApplication app)
        {
            using (var serviceScope = app.Services.CreateScope())
            {
                var policy = serviceScope.ServiceProvider.GetService<ClientPolicy>(); //Polly
                var context = serviceScope.ServiceProvider.GetService<DatabaseContext>(); //EF
                SeedData(policy!, context!);
            }
        }

        private static void SeedData(ClientPolicy policy, DatabaseContext context)
        {
            Console.WriteLine("Applying migrations...");

            policy.MigrationRetryPolicy.Execute(() => context.Database.Migrate());

            //if (!context.Character.Any())
            //{
            //    context.Character.Add(new Character
            //    {
            //        Name = "Zafina",
            //        Description = "Assassin"
            //    });
            //}

            context.SaveChanges();
        }
    }
}
