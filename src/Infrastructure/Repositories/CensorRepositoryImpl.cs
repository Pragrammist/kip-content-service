using Mapster;
using Core;
using Core.Dtos;
using Core.Repositories;
using MongoDB.Driver;

namespace Infrastructure.Repositories;

public class CensorRepositoryImpl : CensorRepository
{
    FilterDefinition<Censor> FilterById(string id) => Builders<Censor>.Filter.Eq(cens => cens.Id, id);
    readonly IMongoCollection<Censor> _censorMongoRepo;
    IMongoCollection<Film> _filmsCol;
    public CensorRepositoryImpl(IMongoCollection<Censor> censorMongoRepo, IMongoCollection<Film> filmsCol)
    {
        _censorMongoRepo = censorMongoRepo;
        _filmsCol = filmsCol;
    }
    public async Task<bool> DeleteFilm(string id, string filmId, CancellationToken token = default) =>
        (await _censorMongoRepo.UpdateOneAsync(
            filter: FilterById(id), 
            update: Builders<Censor>.Update.Pull(cens => cens.Films, filmId), // pull is used to delete. Thanks to mongo driver for this
            cancellationToken: token)).ModifiedCount > 0;
    


    public async Task<bool> ChangeName(string id, string name, CancellationToken token = default) =>    
        (await _censorMongoRepo.UpdateOneAsync(
            filter: FilterById(id), 
            update: Builders<Censor>.Update.Set(cens => cens.Name, name), 
            cancellationToken: token)).ModifiedCount > 0;
            
        
    

    public async Task<CensorDto> Create(string name, CancellationToken token = default, List<string>? films = null) {
        var censor = new Censor(name, films: films);
        await _censorMongoRepo.InsertOneAsync(
            document: censor,
            options: null, 
            cancellationToken: token);
        return censor.Adapt<CensorDto>();
    }

    public async Task<bool> Delete(string id, CancellationToken token = default) =>
        (await _censorMongoRepo.DeleteOneAsync(
            filter: FilterById(id), 
            cancellationToken: token)).DeletedCount > 0;
    

    public async Task<IEnumerable<CensorDto>> Get(uint limit = 20, uint page = 1, CancellationToken token = default) =>
        (await _censorMongoRepo.FindAsync(
            filter: Builders<Censor>.Filter.Empty,
            options: new FindOptions<Censor> // here is projection from Censor to CensorDto
                { 
                    Limit = ((int)limit), 
                    Skip = (int)((page - 1) * limit) 
                },
            cancellationToken: token
        )).ToEnumerable().Adapt<IEnumerable<CensorDto>>();
    

    public async Task<CensorDto?> Get(string id, CancellationToken token = default) =>
        (await _censorMongoRepo.FindAsync(
            filter: FilterById(id),
            options: new FindOptions<Censor>(), // here is projection from Censor to CensorDto
            cancellationToken: token
        )).FirstOrDefault().Adapt<CensorDto>();

    public async Task<bool> SetFilmsTop(string id, List<string> films, CancellationToken token = default) {
        foreach(var film in films)
        {
            var isFound = (await _filmsCol.FindAsync(
                filter: Builders<Film>.Filter.Eq(f => f.Id, film),
                cancellationToken: token
            )).FirstOrDefault() is not null;
            if(!isFound)
                return false;
        }

        return (await _censorMongoRepo.UpdateOneAsync(
            filter: FilterById(id),
            update: Builders<Censor>.Update.Set(cens => cens.Films, films),
            cancellationToken: token
        )).ModifiedCount > 0;

    }
        

}
