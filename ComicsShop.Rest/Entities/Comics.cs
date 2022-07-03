namespace ComicsShop.Rest.Entities;

public class Comics
{
    public Guid Id { get; set; }
    public double? Price { get; set; }
    public string? Title { get; set; }
    public string? Image { get; set; }
    public string? Description { get; set; }
}
