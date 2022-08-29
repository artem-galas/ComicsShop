using ComicsShop.Rest.Mappers;
using ComicsShop.Rest.Services;

namespace ComicsShop.Rest.Endpoints;

public static class GetComicsById
{
    public static async Task<IResult> Handler(IComicsService service, IComicsMapper mapper, string id)
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
}