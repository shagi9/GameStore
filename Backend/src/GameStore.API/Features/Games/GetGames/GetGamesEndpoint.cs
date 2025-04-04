﻿using GameStore.API.Data;

namespace GameStore.API.Features.Games.GetGames
{
    public static class GetGamesEndpoint
    {
        public static void MapGetGames(this IEndpointRouteBuilder app)
        {
            // GET /games
            app.MapGet("/", (GameStoreData data) => data.GetGames.Select(game => new GameSummaryDto(
                game.Id,
                game.Name,
                game.Genre.Name,
                game.Price,
                game.ReleaseDate
            )));
        }
    }
}
