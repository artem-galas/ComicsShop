using ComicsShop.Rest.Mappers;
using ComicsShop.Rest.Services;
using ComicsShop.Rest.Extensions;

namespace ComicsShop.Rest.Endpoints;

public class ComicsModule : IModule
{
    public IServiceCollection RegisterModule(IServiceCollection services)
    {
        services.AddScoped<IComicsService, ComicsService>();
        services.AddScoped<IComicsMapper, ComicsMapper>();

        return services;
    }

    public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/comics", GetAllComics.Handler);
        endpoints.MapGet("/comics/{id}", GetComicsById.Handler);
        endpoints.MapPost("/comics", CreateComics.Handler);
        endpoints.MapPut("/comics/{id}", UpdateComics.Handler);
        endpoints.MapDelete("/comics/{id}", DeleteComicsById.Handler);

        return endpoints;
    }
}