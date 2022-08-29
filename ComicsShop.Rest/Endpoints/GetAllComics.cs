using ComicsShop.Rest.Mappers;
using ComicsShop.Rest.Services;

namespace ComicsShop.Rest.Endpoints;

public static class GetAllComics
{
    public static async Task<IResult> Handler(IComicsService service, IComicsMapper mapper)
    {
        var data = await service.GetAll();

        return Results.Ok(mapper.MapToComicsListResponse(data));
    }
}