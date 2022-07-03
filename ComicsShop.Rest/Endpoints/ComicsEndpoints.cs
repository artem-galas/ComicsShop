using ComicsShop.Rest.Mappers;
using ComicsShop.Rest.Services;
using ComicsShop.Rest.Entities;

namespace ComicsShop.Rest.Endpoints;

public static class ComicsEndpoints
{
    public static void MapComicsEndpoints(this WebApplication app)
    {
        app.MapGet("/comics", GetAllComics);
        app.MapGet("/comics/{id}", GetComicsById);
        app.MapPost("/comics", CreateComics);
        app.MapPut("/comics/{id}", UpdateComics);
        app.MapDelete("/comics/{id}", DeleteComicsById);
        app.MapGet("/smoke", SmokeTesting);
    }

    public static void AddComicsService(this IServiceCollection service)
    {
        service.AddScoped<IComicsService, ComicsService>();
        service.AddScoped<IComicsMapper, ComicsMapper>();
    }

    internal static IResult SmokeTesting()
    {
        return Results.Ok("Hello World");
    }

    internal static async Task<IResult> GetAllComics(IComicsService service, IComicsMapper mapper)
    {
        var data = await service.GetAll();

        return Results.Ok(mapper.MapToComicsListResponse(data));
    }

    internal static async Task<IResult> GetComicsById(IComicsService service, IComicsMapper mapper, string id)
    {
        if (!Guid.TryParse(id, out var newGuid))
        {
            return Results.BadRequest("Id is not GUID");
        }

        var data = await service.GetById(newGuid);

        if (data is null)
        {
            return Results.NotFound("Comics not found");
        }

        return Results.Ok(mapper.MapToComicsResponse(data));
    }

    internal static async Task<IResult> UpdateComics(IComicsService service, IComicsMapper mapper, Comics comics,
        string id)
    {
        if (!Guid.TryParse(id, out var newGuid))
        {
            return Results.BadRequest("Id is not GUID");
        }

        var data = await service.GetById(newGuid);

        if (data is null)
        {
            return Results.NotFound("Comics not found");
        }

        var result = await service.UpdateById(newGuid, comics);

        return Results.Ok(mapper.MapToComicsResponse(result));
    }

    internal static async Task<IResult> CreateComics(IComicsService service, IComicsMapper mapper, Comics comics)
    {
        try
        {
            var data = await service.Create(comics);
            return Results.Created($"/comics/{data.Id}", mapper.MapToComicsResponse(data));
        }
        catch
        {
            return Results.BadRequest("Could not create new entity");
        }
    }

    internal static async Task<IResult> DeleteComicsById(IComicsService service, string id)
    {
        if (!Guid.TryParse(id, out var newGuid))
        {
            return Results.BadRequest("Id is not GUID");
        }

        var data = await service.GetById(newGuid);

        if (data is null)
        {
            return Results.NotFound("Comics not found");
        }

        await service.Delete(newGuid);

        return Results.NoContent();
    }
}