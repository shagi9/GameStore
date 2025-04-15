using System.Data.Entity;
using GameStore.API.Data;

namespace GameStore.API.Features.Games.GetGames
{
    public static class GetGamesEndpoint
    {
        public static void MapGetGames(this IEndpointRouteBuilder app)
        {
            // GET /games
            app.MapGet("/", (GameStoreContext dbContext) =>
            {
                return dbContext.Games
                    .Include(x => x.Genre)
                    .Select(game => new GameSummaryDto(
                        game.Id,
                        game.Name,
                        game.Genre!.Name,
                        game.Price,
                        game.ReleaseDate
                    ))
                    .AsNoTracking();
            });
        }
    }
}
