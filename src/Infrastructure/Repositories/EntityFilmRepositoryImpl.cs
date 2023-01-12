using System.Runtime.InteropServices;
using Core;
using Core.Repositories;
using MongoDB.Driver;

namespace Infrastructure.Repositories;

public class EntityFilmRepositoryImpl : EntityFilmRepository
{
    IMongoCollection<Film> _censorMongoRepo;
    FilterDefinition<Film> FilterById(string id) => Builders<Film>.Filter.Eq(cens => cens.Id, id);
    public EntityFilmRepositoryImpl(IMongoCollection<Film> censorMongoRepo)
    {
        _censorMongoRepo = censorMongoRepo;
    }
    public async Task<Film?> Get(string id, CancellationToken token) =>
        (await _censorMongoRepo.FindAsync(
            filter: FilterById(id),
            cancellationToken: token
        )).FirstOrDefault();
        
}