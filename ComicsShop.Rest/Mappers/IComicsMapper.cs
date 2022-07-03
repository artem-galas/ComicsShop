using ComicsShop.Rest.Entities;

namespace ComicsShop.Rest.Mappers;

public class ComicsResponse
{
    public Guid Id { get; set; }
    public double? Price { get; set; }
    public string? Title { get; set; }
    public string? Image { get; set; }
    public string? Description { get; set; }
}

public interface IComicsMapper
{
    ComicsResponse MapToComicsResponse(Comics entity);
    IEnumerable<ComicsResponse> MapToComicsListResponse(IEnumerable<Comics> entity);
}