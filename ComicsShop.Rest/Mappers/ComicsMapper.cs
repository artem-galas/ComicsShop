using ComicsShop.Rest.Entities;

namespace ComicsShop.Rest.Mappers;

public class ComicsMapper : IComicsMapper
{
    public ComicsResponse MapToComicsResponse(Comics entity)
    {
        return new ComicsResponse
        {
            Id = entity.Id,
            Title = entity.Title,
            Description = entity.Description,
            Image = entity.Image,
            Price = entity.Price
        };
    }

    public IEnumerable<ComicsResponse> MapToComicsListResponse(IEnumerable<Comics> entity)
    {
        var response = new List<ComicsResponse>();
        
        entity.ToList().ForEach(c =>
        {
            response.Add(MapToComicsResponse(c));
        });

        return response;
    }
}