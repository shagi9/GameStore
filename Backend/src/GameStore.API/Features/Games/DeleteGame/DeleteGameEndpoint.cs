using GameStore.API.Data;
using Microsoft.EntityFrameworkCore;

namespace GameStore.API.Features.Games.DeleteGame
{
    public static class DeleteGameEndpoint
    {
        public static void MapDeleteGame(this IEndpointRouteBuilder app)
        {
            // DELETE // /games/122
            app.MapDelete("/{id}", (Guid id, GameStoreContext dbContext) =>
            {
                dbContext.Games
                    .Where(x => x.Id == id)
                    .ExecuteDelete();
 
                return Results.NoContent();
            });
        }
    }
}
