﻿using GameStore.API.Data;
using GameStore.API.Features.Constants;
using GameStore.API.Models;

namespace GameStore.API.Features.Games.GetGame
{
    public static class GetGameEndpoint
    {
        public static void MapGetGame(
            this IEndpointRouteBuilder app,
            GameStoreData data
            )
        {
            // GET /games/id
            app.MapGet("/games/{id}", (Guid id) => {
                Game? game = data.GetGame(id);

                return game is null ? Results.NotFound(game) : Results.Ok(
                    new GameDetailsDto(
                        game.Id,
                        game.Name,
                        game.Genre.Id,
                        game.Price,
                        game.ReleaseDate,
                        game.Description)
                );
            }).WithName(EndpointNames.GetGame);
        }
    }
}
