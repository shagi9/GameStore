using System.ComponentModel.DataAnnotations;
using GameStore.API.Data;
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

GameStoreData data = new();

// GET /games
app.MapGet("/games", () => data.GetGames.Select(game => new GameSummaryDto(
    game.Id,
    game.Name,
    game.Genre.Name,
    game.Price,
    game.ReleaseDate
)));

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
})
.WithName(GetGameEndpointName);

// POST / games
app.MapPost("/games", (CreateGameDto gameDto) =>
{
    var genre = data.GetGenre(gameDto.GenreId);

    if (genre is null)
    {
        return Results.BadRequest("Invalid Genre Id");
    }

    var newGame = new Game
    {
        Name = gameDto.Name,
        Genre = genre,
        Price = gameDto.Price,
        ReleaseDate = gameDto.ReleaseDate,
        Description = gameDto.Description
    };

    data.AddGame(newGame);

    return Results.CreatedAtRoute(
        GetGameEndpointName, 
        new { id = newGame.Id }, 
        new GameDetailsDto(
            newGame.Id,
            newGame.Name,
            newGame.Genre.Id,
            newGame.Price,
            newGame.ReleaseDate,
            newGame.Description
        ));
})
.WithParameterValidation();

// PUT // /games/122
app.MapPut("/games/{id}", (Guid id, UpdateGameDto gameDto) =>
{
    Game? existingGame = data.GetGame(id);

    if (existingGame is null)
    {
        return Results.NotFound();
    }

    var genre = data.GetGenre(existingGame.Id);

    if (genre is null)
    {
        return Results.BadRequest("Invalid Genre Id");
    }

    existingGame.Name = gameDto.Name;
    existingGame.Genre = genre;
    existingGame.Price = gameDto.Price;
    existingGame.ReleaseDate = gameDto.ReleaseDate;
    existingGame.Description = gameDto.Description;

    return Results.NoContent();
});

// DELETE // /games/122
app.MapDelete("/games/{id}", (Guid id) =>
{
    data.RemoveGame(id);

    return Results.NoContent();
});

// GET /genres
app.MapGet("/genres", () => data.GetGenres.Select(genre => new GenreDto(
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

public record CreateGameDto(
    [Required][StringLength(50)] string Name,
     Guid GenreId,
    [Range(1, 100)] decimal Price,
    DateOnly ReleaseDate,
    [Required][StringLength(500)] string Description
);

public record UpdateGameDto(
    [Required][StringLength(50)] string Name,
     Guid GenreId,
    [Range(1, 100)] decimal Price,
    DateOnly ReleaseDate,
    [Required][StringLength(500)] string Description
);

public record GenreDto(Guid Id, string Name);