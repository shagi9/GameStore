using GameStore.API.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options => // UseSwaggerUI is called only in Development.
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
}

const string GetGameEndpointName = "GetGame";

List<Game> games =
[
   new Game() {
       Id = Guid.NewGuid(),
       Name = "Street Fighter II",
       Genre = "Fighting",
       Price = 19.99m,
       ReleaseDate = new DateOnly(1992, 7, 15)
   },
   new Game() {
       Id = Guid.NewGuid(),
       Name = "Final Fantasy XIV",
       Genre = "Roleplaying",
       Price = 59.99m,
       ReleaseDate = new DateOnly(2010, 9, 30)
   },
   new Game() {
       Id = Guid.NewGuid(),
       Name = "FIFA 23",
       Genre = "Sports",
       Price = 69.99m,
       ReleaseDate = new DateOnly(2022, 9, 27)
   }
];

// GET /games
app.MapGet("/games", () => games);

// GET /games/id
app.MapGet("/games/{id}", (Guid id) => {
    Game? game = games.Find(x => x.Id == id);

    return game is null ? Results.NotFound(game) : Results.Ok(game);
})
.WithName(GetGameEndpointName);

// POST / games
app.MapPost("/games", (Game game) =>
{
    game.Id = Guid.NewGuid();
    games.Add(game);

    return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.Id }, game);
})
.WithParameterValidation();

// PUT // /games/122
app.MapPut("/games/{id}", (Guid id, Game updatedGame) =>
{
    Game? existingGame = games.Find(x => x.Id == id);

    if (existingGame is null)
    {
        return Results.NotFound();
    }

    existingGame.Name = updatedGame.Name;
    existingGame.Genre = updatedGame.Genre;
    existingGame.Price = updatedGame.Price;
    existingGame.ReleaseDate = updatedGame.ReleaseDate;

    return Results.NoContent();
});

// DELETE // /games/122
app.MapDelete("/games/{id}", (Guid id) =>
{
    games.RemoveAll(x => x.Id == id);

    return Results.NoContent();
});

await app.RunAsync();
