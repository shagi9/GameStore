using GameStore.API.Data;

namespace GameStore.API.Features.Genres.GetGenres
{
    public static class GetGenresEndpoint
    {
        public static void MapGetGenres(
           this IEndpointRouteBuilder app,
           GameStoreData data)
        {
            // GET /genres
            app.MapGet("/", () => data.GetGenres.Select(genre => new GenreDto(
                genre.Id,
                genre.Name
            )));
        }
    }
}
