using GameStore.API.Data;
using GameStore.API.Features.Constants;
using GameStore.API.Models;

namespace GameStore.API.Features.Games.GetGame
{
    public static class GetGameEndpoint
    {
        public static void MapGetGame(this IEndpointRouteBuilder app)
        {
            // GET /games/id
            app.MapGet("/{id}", (Guid id, GameStoreData data) => {
                Game? game = data.GetGame(id);

                return game is null ? Results.NotFound(game) : Results.Ok(
                    new GameDetailsDto(
                        game.Id,
                        game.Name,
                        game.GenreId,
                        game.Price,
                        game.ReleaseDate,
                        game.Description)
                );
            }).WithName(EndpointNames.GetGame);
        }
    }
}
