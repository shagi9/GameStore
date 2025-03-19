using GameStore.API.Data;
using GameStore.API.Features.Games.AddGame;
using GameStore.API.Features.Games.DeleteGame;
using GameStore.API.Features.Games.GetGame;
using GameStore.API.Features.Games.GetGames;
using GameStore.API.Features.Games.UpdateGame;

namespace GameStore.API.Features.Games
{
    public static class GamesEndpoints
    {
        public static void MapGames(
            this IEndpointRouteBuilder app,
            GameStoreData data
        )
        {
            var group = app.MapGroup("/games");

            group.MapGetGames(data);
            group.MapGetGame(data);
            group.MapAddGame(data);
            group.MapUpdateGame(data);
            group.MapDeleteGame(data);
        }
    }
}
