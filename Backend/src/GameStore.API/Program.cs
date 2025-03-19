using GameStore.API.Data;
using GameStore.API.Features.Games.AddGame;
using GameStore.API.Features.Games.DeleteGame;
using GameStore.API.Features.Games.GetGame;
using GameStore.API.Features.Games.GetGames;
using GameStore.API.Features.Games.UpdateGame;
using GameStore.API.Features.Genres.GetGenres;

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

GameStoreData data = new();

app.MapGetGames(data);
app.MapGetGame(data);
app.MapAddGame(data);
app.MapUpdateGame(data);
app.MapDeleteGame(data);
app.MapGetGenres(data);

await app.RunAsync();