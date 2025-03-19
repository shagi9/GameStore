using GameStore.API.Data;
using GameStore.API.Models;

namespace GameStore.API.Features.Games.UpdateGame
{
    public static class UpdateGameEndpoint
    {
        public static void MapUpdateGame(
            this IEndpointRouteBuilder app,
            GameStoreData data
            )
        {
            // PUT // /games/122
            app.MapPut("/games/{id}", (Guid id, UpdateGameDto gameDto) =>
            {
                Game? existingGame = data.GetGame(id);

                if (existingGame is null)
                {
                    return Results.NotFound();
                }

                var genre = data.GetGenre(existingGame.Id);

                if (genre is null)
                {
                    return Results.BadRequest("Invalid Genre Id");
                }

                existingGame.Name = gameDto.Name;
                existingGame.Genre = genre;
                existingGame.Price = gameDto.Price;
                existingGame.ReleaseDate = gameDto.ReleaseDate;
                existingGame.Description = gameDto.Description;

                return Results.NoContent();
            });
        }
    }
}
