using GameStore.API.Data;
using GameStore.API.Features.Constants;
using GameStore.API.Models;

namespace GameStore.API.Features.Games.AddGame
{
    public static class AddGameEndpoint
    {
        public static void MapAddGame(this IEndpointRouteBuilder app)
        {
            // POST / games
            app.MapPost("/", (CreateGameDto gameDto, GameStoreContext dbContext) =>
            {
                var newGame = new Game
                {
                    Name = gameDto.Name,
                    GenreId = gameDto.GenreId,
                    Price = gameDto.Price,
                    ReleaseDate = gameDto.ReleaseDate,
                    Description = gameDto.Description
                };

                dbContext.Games.Add(newGame);
                dbContext.SaveChanges();

                return Results.CreatedAtRoute(
                    EndpointNames.GetGame,
                    new { id = newGame.Id },
                    new GameDetailsDto(
                        newGame.Id,
                        newGame.Name,
                        newGame.GenreId,
                        newGame.Price,
                        newGame.ReleaseDate,
                        newGame.Description
                    ));
            }).WithParameterValidation();
        }
    }
}
