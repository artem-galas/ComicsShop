using ComicsShop.Rest.Database;
using ComicsShop.Rest.Extensions;
using ComicsShop.Rest.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.RegisterModules();
builder.Services.AddDbConnection();
builder.Services.AddComicsRepository();

var app = builder.Build();

app.UseApiResponse();

app.MapEndpoints();

app.Run();