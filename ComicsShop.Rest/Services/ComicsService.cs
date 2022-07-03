using ComicsShop.Rest.Entities;
using ComicsShop.Rest.Repositories;

namespace ComicsShop.Rest.Services;

public class ComicsService : IComicsService
{
    private readonly IComicsRepository _repository;
    public ComicsService(IComicsRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<IEnumerable<Comics>> GetAll()
    {
        var result = await _repository.GetAll();

        return result;
    }

    public async Task<Comics?> GetById(Guid id)
    {
        var result = await _repository.GetById(id);

        return result;
    }

    public async Task<Comics> UpdateById(Guid id, Comics comics)
    {
        var current = await _repository.GetById(id);

        var data = new Comics
        {
            Id = id,
            Description = comics.Description ?? current.Description,
            Image = comics.Image ?? current.Image,
            Title = comics.Title ?? current.Title,
            Price = comics.Price ?? current.Price,
        };
        
        var result = await _repository.UpdateById(id, data);
        
        var updatedComics = await _repository.GetById(id);

        return updatedComics;
    }

    public async Task<Comics> Create(Comics comics)
    {
        var newComicsId = await _repository.Create(new Comics()
        {
            Description = comics.Description,
            Image = comics.Image,
            Price = comics.Price,
            Title = comics.Title,
        });

        return await _repository.GetById(newComicsId);
    }

    public async Task Delete(Guid id)
    {
        await _repository.DeleteById(id);
    }
}