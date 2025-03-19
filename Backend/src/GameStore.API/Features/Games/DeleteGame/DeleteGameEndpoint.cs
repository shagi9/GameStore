using GameStore.API.Data;

namespace GameStore.API.Features.Games.DeleteGame
{
    public static class DeleteGameEndpoint
    {
        public static void MapDeleteGame(
            this IEndpointRouteBuilder app,
            GameStoreData data
        )
        {
            // DELETE // /games/122
            app.MapDelete("/{id}", (Guid id) =>
            {
                data.RemoveGame(id);

                return Results.NoContent();
            });
        }
    }
}
