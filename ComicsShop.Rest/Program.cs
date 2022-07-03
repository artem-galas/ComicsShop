using ComicsShop.Rest.Database;
using ComicsShop.Rest.Endpoints;
using ComicsShop.Rest.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddComicsService();
builder.Services.AddDbConnection();
builder.Services.AddComicsRepository();

var app = builder.Build();

app.UseApiResponse();

app.MapComicsEndpoints();

app.Run();