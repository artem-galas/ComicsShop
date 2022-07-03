using ComicsShop.Rest.Entities;

namespace ComicsShop.Rest.Services;

public interface IComicsService
{
    Task<IEnumerable<Comics>> GetAll();
    Task<Comics?> GetById(Guid id);
    Task<Comics> UpdateById(Guid id, Comics comics);
    Task<Comics> Create(Comics comics);
    Task Delete(Guid id);
}
