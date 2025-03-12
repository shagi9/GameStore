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

List<Genre> genres =
[
    new Genre {
        Id = new Guid("4e179397-c3f1-45ec-a271-c26f07ff64f3"),
        Name = "Fighting"
    },
    new Genre {
        Id = new Guid("b2c3d4e5-f678-90a1-b2c3-d4e5f67890a1"),
        Name = "Kids and Family"
    },
    new Genre {
        Id = new Guid("c3d4e5f6-7890-a1b2-c3d4-e5f67890a1b2"),
        Name = "Racing"
    },
    new Genre {
        Id = new Guid("d4e5f678-90a1-b2c3-d4e5-f67890a1b2c3"),
        Name = "Roleplaying"
    },
    new Genre {
        Id = new Guid("e5f67890-a1b2-c3d4-e5f6-7890a1b2c3d4"),
        Name = "Sports"
    },
];

List<Game> games =
[
   new Game {
       Id = Guid.NewGuid(),
       Name = "Street Fighter II",
       Genre = genres[0],
       Price = 19.99m,
       ReleaseDate = new DateOnly(1992, 7, 15),
       Description = "Street Fighter 2, the most iconic fighting game of all time, is back on the Nintendo Switch! The newest iteration of SFII in nearly 10 years, Ultra Street Fighter 2 features all of the classic characters, a host of new single player and multiplayer features, as well as two new fighters: Evil Ryu and Violent Ken!"
   },
   new Game {
       Id = Guid.NewGuid(),
       Name = "Final Fantasy XIV",
       Genre = genres[3],
       Price = 59.99m,
       ReleaseDate = new DateOnly(2010, 9, 30),
       Description = "Join over 27 million adventurers worldwide and take part in an epic and ever-changing FINAL FANTASY. Experience an unforgettable story, exhilarating battles, and a myriad of captivating environments to explore."
   },
   new Game {
       Id = Guid.NewGuid(),
       Name = "FIFA 23",
       Genre = genres[4],
       Price = 69.99m,
       ReleaseDate = new DateOnly(2022, 9, 27),
       Description = "FIFA 23 brings The World's Game to the pitch, with HyperMotion2 Technology, men's and women's FIFA World Cup™, women's club teams, cross-play features, and more."
   }
];

// GET /games
app.MapGet("/games", () => games.Select(game => new GameSummaryDto(
    game.Id,
    game.Name,
    game.Genre.Name,
    game.Price,
    game.ReleaseDate
)));

// GET /games/id
app.MapGet("/games/{id}", (Guid id) => {
    Game? game = games.Find(x => x.Id == id);

    return game is null ? Results.NotFound(game) : Results.Ok(
        new GameDetailsDto(
            game.Id,
            game.Name,
            game.Genre.Id,
            game.Price,
            game.ReleaseDate,
            game.Description)
    );
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

// GET /genres
app.MapGet("/genres", () => genres.Select(genre => new GenreDto(
    genre.Id,
    genre.Name
)));

await app.RunAsync();

public record GameDetailsDto(
    Guid Id, 
    string Name, 
    Guid GenreId, 
    decimal Price,
    DateOnly ReleaseDate,
    string Description);

public record GameSummaryDto(
    Guid Id,
    string Name,
    string Genre,
    decimal Price,
    DateOnly ReleaseDates
);

public record GenreDto(Guid Id, string Name);