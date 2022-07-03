using ComicsShop.Rest.Entities;
using SqlKata.Execution;

namespace ComicsShop.Rest.Repositories;

public class ComicsRepository : IComicsRepository
{
    private readonly QueryFactory _db;

    public ComicsRepository(QueryFactory db)
    {
        _db = db;
    }

    public Task<Comics> GetById(Guid id)
    {
        return _db.Query("Comics").Where("Id", id).FirstOrDefaultAsync<Comics>();
    }

    public Task<IEnumerable<Comics>> GetAll()
    {
        return _db.Query("Comics").GetAsync<Comics>();
    }

    public async Task<Guid> UpdateById(Guid id, Comics comics)
    {
        await _db.Query("Comics").Where("Id", id).UpdateAsync(comics);

        return id;
    }

    public async Task<Guid> Create(Comics comics)
    {
        comics.Id = Guid.NewGuid();
        await _db.Query("Comics").InsertAsync(comics);
        return comics.Id;
    }

    public Task DeleteById(Guid id)
    {
        return _db.Query("Comics").Where("Id", id).DeleteAsync();
    }
}