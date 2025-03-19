using GameStore.API.Data;
using GameStore.API.Features.Games;
using GameStore.API.Features.Genres;

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

app.MapGames(data);
app.MapGenres(data);

await app.RunAsync();