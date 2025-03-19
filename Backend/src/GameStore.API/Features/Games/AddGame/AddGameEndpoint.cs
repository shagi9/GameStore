using GameStore.API.Data;
using GameStore.API.Features.Constants;
using GameStore.API.Models;

namespace GameStore.API.Features.Games.AddGame
{
    public static class AddGameEndpoint
    {
        public static void MapAddGame(
            this IEndpointRouteBuilder app,
            GameStoreData data
            )
        {
            // POST / games
            app.MapPost("/games", (CreateGameDto gameDto) =>
            {
                var genre = data.GetGenre(gameDto.GenreId);

                if (genre is null)
                {
                    return Results.BadRequest("Invalid Genre Id");
                }

                var newGame = new Game
                {
                    Name = gameDto.Name,
                    Genre = genre,
                    Price = gameDto.Price,
                    ReleaseDate = gameDto.ReleaseDate,
                    Description = gameDto.Description
                };

                data.AddGame(newGame);

                return Results.CreatedAtRoute(
                    EndpointNames.GetGame,
                    new { id = newGame.Id },
                    new GameDetailsDto(
                        newGame.Id,
                        newGame.Name,
                        newGame.Genre.Id,
                        newGame.Price,
                        newGame.ReleaseDate,
                        newGame.Description
                    ));
            }).WithParameterValidation();
        }
    }
}
