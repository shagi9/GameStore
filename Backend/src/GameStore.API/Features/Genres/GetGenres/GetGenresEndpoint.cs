using GameStore.API.Data;
using System.Data.Entity;

namespace GameStore.API.Features.Genres.GetGenres
{
    public static class GetGenresEndpoint
    {
        public static void MapGetGenres(this IEndpointRouteBuilder app)
        {
            // GET /genres
            app.MapGet("/", (GameStoreContext dbContext) => dbContext.Genres
                .Select(genre => new GenreDto(genre.Id,genre.Name))
                .AsNoTracking());
        }
    }
}
