using ComicsShop.Rest.Services;

namespace ComicsShop.Rest.Endpoints;

public class DeleteComicsById
{
    public static async Task<IResult> Handler(IComicsService service, string id)
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