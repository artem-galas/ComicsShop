using ComicsShop.Rest.Entities;
using ComicsShop.Rest.Mappers;
using ComicsShop.Rest.Services;

namespace ComicsShop.Rest.Endpoints;

public static class CreateComics
{
    public static async Task<IResult> Handler(IComicsService service, IComicsMapper mapper, Comics comics)
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
}