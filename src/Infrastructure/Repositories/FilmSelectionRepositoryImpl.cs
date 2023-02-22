using Mapster;
using Core;
using Core.Dtos;
using Core.Repositories;
using MongoDB.Driver;

namespace Infrastructure.Repositories;

public class FilmSelectionRepositoryImpl : FilmSelectionRepository
{
    FilterDefinition<FilmSelection> FilterById(string id) => Builders<FilmSelection>.Filter.Eq(cens => cens.Id, id);
    readonly IMongoCollection<FilmSelection> _filmSelectionCol;
    IMongoCollection<Film> _filmsCol;
    public FilmSelectionRepositoryImpl(IMongoCollection<FilmSelection> filmSelectionCol, IMongoCollection<Film> filmsCol)
    {
        _filmSelectionCol = filmSelectionCol;
        _filmsCol = filmsCol;
    }
    public async Task<bool> DeleteFilm(string id, string filmId, CancellationToken token = default) =>
        (await _filmSelectionCol.UpdateOneAsync(
            filter: FilterById(id), 
            update: Builders<FilmSelection>.Update.Pull(cens => cens.Films, filmId), // pull is used to delete. Thanks to mongo driver for this
            cancellationToken: token)).ModifiedCount > 0;
    


    public async Task<bool> ChangeName(string id, string name, CancellationToken token = default) =>    
        (await _filmSelectionCol.UpdateOneAsync(
            filter: FilterById(id), 
            update: Builders<FilmSelection>.Update.Set(cens => cens.Name, name), 
            cancellationToken: token)).ModifiedCount > 0;
            
        
    

    public async Task<FilmSelectionDto?> Create(string name, CancellationToken token = default, List<string>? films = null) {
        foreach(var film in films ?? new List<string>())
        {
            var isFound = (await _filmsCol.FindAsync(
                filter: Builders<Film>.Filter.Eq(f => f.Id, film),
                cancellationToken: token
            )).FirstOrDefault() is not null;
            if(!isFound)
                return null;
        }
        var filmSelection = new FilmSelection(name, films: films);
        await _filmSelectionCol.InsertOneAsync(
            document: filmSelection,
            options: null, 
            cancellationToken: token);
        return filmSelection.Adapt<FilmSelectionDto>();
    }


    public async Task<bool> Delete(string id, CancellationToken token = default) =>
        (await _filmSelectionCol.DeleteOneAsync(
            filter: FilterById(id), 
            cancellationToken: token)).DeletedCount > 0;
    

    public async Task<IEnumerable<FilmSelectionDto>> Get(uint limit = 20, uint page = 1, CancellationToken token = default) =>
        (await _filmSelectionCol.FindAsync(
            filter: Builders<FilmSelection>.Filter.Empty,
            options: new FindOptions<FilmSelection> // here is projection from Censor to CensorDto
                { 
                    Limit = ((int)limit), 
                    Skip = (int)((page - 1) * limit) 
                },
            cancellationToken: token
        )).ToEnumerable().Adapt<IEnumerable<FilmSelectionDto>>();
    

    public async Task<FilmSelectionDto?> Get(string id, CancellationToken token = default) =>
        (await _filmSelectionCol.FindAsync(
            filter: FilterById(id),
            options: new FindOptions<FilmSelection>(), // here is projection from Censor to CensorDto
            cancellationToken: token
        )).FirstOrDefault().Adapt<FilmSelectionDto>();

    public async Task<bool> AddFilm(string id, string filmId, CancellationToken token = default) 
    {
        var isExists = (await _filmsCol.FindAsync(
            filter: Builders<Film>.Filter.Eq(f => f.Id, filmId), 
            cancellationToken: token)
        )
        .FirstOrDefault() is not null; // find get coll res and i check if any exists in coll
        
        if(!isExists)
            return false;

        var filmIsAdded = (await _filmSelectionCol.UpdateOneAsync(
            filter: FilterById(id),
            update: Builders<FilmSelection>.Update.AddToSet(f => f.Films, filmId),
            cancellationToken: token
        )).ModifiedCount > 0;

        return filmIsAdded;
    }
        

}
