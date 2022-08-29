using ComicsShop.Rest.Entities;
using ComicsShop.Rest.Mappers;
using ComicsShop.Rest.Services;

namespace ComicsShop.Rest.Endpoints;

public static class UpdateComics
{
    public static async Task<IResult> Handler(IComicsService service, IComicsMapper mapper, Comics comics,
        string id)
    {
        if(!Guid.TryParse(id, out var newGuid))
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
}