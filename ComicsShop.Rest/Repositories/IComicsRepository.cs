using ComicsShop.Rest.Entities;

namespace ComicsShop.Rest.Repositories;

public interface IComicsRepository
{
    Task<Comics> GetById(Guid id);
    Task<IEnumerable<Comics>> GetAll();

    Task<Guid> UpdateById(Guid id, Comics comics);
    Task<Guid> Create(Comics comics);

    Task DeleteById(Guid id);
}