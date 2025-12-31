using GameStore.Api.Dtos;

var builder = WebApplication.CreateBuilder(args);

const string GetGameEndpointName = "GetGame";

var app = builder.Build();

List<GameDto> games = [
    new (
        1,
        "Street fighter II",
        "Fighting",
        19.99M,
        new DateOnly(1992, 7, 15)),

    new (
        2,
        "Grang Thief Auto",
        "Race",
        16.79M,
        new DateOnly(1999, 3, 5)),

    new (
        3,
        "Street fighter III",
        "Fighting",
        49.99M,
        new DateOnly(1995, 6, 8)),

    new (
        4,
        "NBA 2K",
        "Sport",
        194.99M,
        new DateOnly(2023, 5, 6)),

    new (
        5,
        "NHL 16",
        "Sport",
        139.99M,
        new DateOnly(2016, 3, 1)),
];

app.MapGet("/games", () => games);


app.MapGet("/games/{id}", (int id) => 
{
    var game = games.Find(game => game.Id == id);

    return game is null ? Results.NotFound() : Results.Ok(game);
})
.WithName(GetGameEndpointName);


app.MapPost("/games", (CreateGameDto newGame) => 
{
    GameDto game = new(
        games.Count + 1,
        newGame.Name,
        newGame.Genre,
        newGame.Price,
        newGame.ReleaseDate
    );

    games.Add(game);
    return Results.CreatedAtRoute(GetGameEndpointName, new {id = game.Id}, game);
});


app.MapPut("/games/{id}", (int id, UpdateGameDto updateGame) => 
{
    var index = games.FindIndex(game => game.Id == id);

    if (index == -1){ return Results.NotFound(); }

    games[index] = new GameDto(
        id,
        updateGame.Name,
        updateGame.Genre,
        updateGame.Price,
        updateGame.ReleaseDate
    );

    return Results.NoContent();
});


app.MapDelete("/games/{id}", (int id) => {
    games.RemoveAll(game => game.Id == id);

    return Results.NoContent();
});

app.Run();
