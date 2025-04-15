using Microsoft.EntityFrameworkCore;
using GameStore.API.Models;

namespace GameStore.API.Data
{
    public static class DataExtensions
    {
        public static void InitDb(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            GameStoreContext dbContext = scope.ServiceProvider.GetRequiredService<GameStoreContext>();

            // apply db migrations automatically when an application starts
            dbContext.Database.Migrate();
            // seed db with data
            SeedDb(dbContext);
        }

        private static void SeedDb(GameStoreContext dbContext)
        {
            if (!dbContext.Genres.Any())
            {
                dbContext.Genres.AddRange(
                    new Genre { Name = "Fighting" },
                    new Genre { Name = "Kids and Family" },
                    new Genre { Name = "Racing" },
                    new Genre { Name = "Roleplaying" },
                    new Genre { Name = "Sports" }
                );

                dbContext.SaveChanges();
            }
        }
    }
}
