using Core;
using Core.Dtos;
using Core.Repositories;
using MongoDB.Driver;
using Core.Dtos.CreateFilmDtos;
using Core.Dtos.UpdateFilmDtos;
using Core.Dtos.UpdateSeasonsDtos;
using Mapster;

namespace Infrastructure.Repositories;

public class FilmRepositoryImpl : FilmRepository
{
    FilterDefinition<Film> FilterById(string id) => Builders<Film>.Filter.Eq(cens => cens.Id, id);

    FilterDefinition<Person> PersonFilterId(string id) => Builders<Person>.Filter.Eq(f => f.Id, id);
    readonly IMongoCollection<Film> _filmsCol;
    IMongoCollection<Person> _personsCol;
    public FilmRepositoryImpl(IMongoCollection<Film> censorMongoRepo, IMongoCollection<Person> personsCol)
    {
        _filmsCol = censorMongoRepo;
        _personsCol = personsCol;
    }

    public async Task<FilmDto> Create(CreateFilmDto film, CancellationToken token = default) 
    {
        var filmToInsert = film.Adapt<Film>();
        await _filmsCol.InsertOneAsync(filmToInsert);
        return filmToInsert.Adapt<FilmDto>();
    }
        
        

    public async Task<IEnumerable<FilmDto>> Get(uint limit = 20, uint page = 1, CancellationToken token = default) =>
        (await _filmsCol.FindAsync(
            filter: Builders<Film>.Filter.Empty,
            options: new FindOptions<Film>{
                Limit = (int)limit,
                Skip = (int)(limit * (page - 1))
            },
            cancellationToken: token
        ))
        .ToEnumerable()
        .Adapt<IEnumerable<FilmDto>>();


    public async Task<FilmDto?> Get(string id, CancellationToken token = default) =>
        (await _filmsCol.FindAsync(
            filter: FilterById(id),
            cancellationToken: token
        )).FirstOrDefault().Adapt<FilmDto>();
    

    public async Task<bool> UpdateData(string id, UpdateFilmDto film, CancellationToken token = default) =>
        (await _filmsCol.UpdateOneAsync(
            filter: FilterById(id),
            update: BuildUpdateDefition(film),
            cancellationToken: token
        )).ModifiedCount > 0;
    

    UpdateDefinition<Film> BuildUpdateDefition(UpdateFilmDto film)
    {
        var updList = new List<UpdateDefinition<Film>>();
        
        

        if(film.AgeLimit is not null)
            updList.Add(Builders<Film>.Update.Set(f => f.AgeLimit, film.AgeLimit));
        
        if(film.Banner is not null)
            updList.Add(Builders<Film>.Update.Set(f => f.Banner, film.Banner));

        if(film.Name is not null)
            updList.Add(Builders<Film>.Update.Set(f => f.Name, film.Name));
        
        if(film.Description is not null)
            updList.Add(Builders<Film>.Update.Set(f => f.Description, film.Description));

        if(film.Country is not null)
            updList.Add(Builders<Film>.Update.Set(f => f.Country, film.Country));
        
        if(film.KindOfFilm is not null)
            updList.Add(Builders<Film>.Update.Set(f => f.KindOfFilm, (FilmType)film.KindOfFilm));
        
        if(film.ReleaseType is not null)
            updList.Add(Builders<Film>.Update.Set(f => f.ReleaseType, (FilmReleaseType)film.ReleaseType));

        if(film.Duration is not null)
            updList.Add(Builders<Film>.Update.Set(f => f.Duration, film.Duration));

        if(film.Release is not null)
            updList.Add(Builders<Film>.Update.Set(f => f.Release, film.Release));

        if(film.StartScreening is not null)
            updList.Add(Builders<Film>.Update.Set(f => f.StartScreening, film.StartScreening));

        if(film.EndScreening is not null)
            updList.Add(Builders<Film>.Update.Set(f => f.EndScreening, film.EndScreening));

        if(film.Content is not null)
            updList.Add(Builders<Film>.Update.Set(f => f.Content, film.Content));

        if(film.Fees is not null)
            updList.Add(Builders<Film>.Update.Set(f => f.Fees, film.Fees));
        
        UpdateDefinition<Film> finalUpdate = Builders<Film>.Update.Combine(updList);
        return finalUpdate;
    }
    

    public async Task<bool> Delete(string id, CancellationToken token = default) =>
        (await _filmsCol.DeleteOneAsync(
            filter: FilterById(id),
            cancellationToken: token
        )).DeletedCount > 0;


    public async Task<bool> AddImage(string id, string image, CancellationToken token = default) =>
        (await _filmsCol.UpdateOneAsync(
            filter: FilterById(id),
            update: Builders<Film>.Update.AddToSet(f => f.Images, image),
            cancellationToken: token
        )).ModifiedCount > 0;
    public async Task<bool> DeleteImage(string id, string image, CancellationToken token = default) =>
        (await _filmsCol.UpdateOneAsync(
            filter: FilterById(id),
            update: Builders<Film>.Update.Pull(f => f.Images, image),
            cancellationToken: token
        )).ModifiedCount > 0;


    public async Task<bool> AddPerson(string id, string personId, CancellationToken token = default)
    {
        var isExists = (await _personsCol.FindAsync(
            filter: PersonFilterId(personId), 
            cancellationToken: token)
        )
        .FirstOrDefault() is not null; // find get coll res and i check if any exists in coll
        
        if(!isExists)
            return false;

        var personIsAddedToFilm = (await _filmsCol.UpdateOneAsync(
            filter: FilterById(id),
            update: Builders<Film>.Update.AddToSet(f => f.Stuff, personId),
            cancellationToken: token
        )).ModifiedCount > 0;

        if(!personIsAddedToFilm)
            return false;

        var filmIsAddedToPerson = (await _personsCol.FindOneAndUpdateAsync(
            filter: PersonFilterId(personId),
            update: Builders<Person>.Update.AddToSet(f => f.Films, id),
            cancellationToken: token
        )) is not null;

        return filmIsAddedToPerson;
    }
        
    public async Task<bool> DeletePerson(string id, string personId, CancellationToken token = default) 
    {
        var personIsDeletedFromFilm = (await _filmsCol.UpdateOneAsync(
            filter: FilterById(id),
            update: Builders<Film>.Update.Pull(f => f.Stuff, personId),
            cancellationToken: token
        )).ModifiedCount > 0;

        if(!personIsDeletedFromFilm)
            return false;

        var filmIsDeletedFromPerson = (await _personsCol.UpdateOneAsync(
            filter: PersonFilterId(personId),
            update: Builders<Person>.Update.Pull(f => f.Films, id),
            cancellationToken: token
        )).ModifiedCount > 0;

        return filmIsDeletedFromPerson;
    }
        

    public async Task<bool> AddArticle(string id, string article, CancellationToken token = default) =>
        (await _filmsCol.UpdateOneAsync(
            filter: FilterById(id),
            update: Builders<Film>.Update.AddToSet(f => f.Articles, article),
            cancellationToken: token
        )).ModifiedCount > 0;
    public async Task<bool> DeleteArticle(string id, string article, CancellationToken token = default) =>
        (await _filmsCol.UpdateOneAsync(
            filter: FilterById(id),
            update: Builders<Film>.Update.Pull(f => f.Articles, article),
            cancellationToken: token
        )).ModifiedCount > 0;


    public async Task<bool> AddTrailer(string id, string trailer, CancellationToken token = default) =>
        (await _filmsCol.UpdateOneAsync(
            filter: FilterById(id),
            update: Builders<Film>.Update.AddToSet(f => f.Trailers, trailer),
            cancellationToken: token
        )).ModifiedCount > 0;   
    public async Task<bool> DeleteTrailer(string id, string trailer, CancellationToken token = default) =>
        (await _filmsCol.UpdateOneAsync(
            filter: FilterById(id),
            update: Builders<Film>.Update.Pull(f => f.Trailers, trailer),
            cancellationToken: token
        )).ModifiedCount > 0;


    public async Task<bool> AddTizer(string id, string tizer, CancellationToken token = default) =>
        (await _filmsCol.UpdateOneAsync(
            filter: FilterById(id),
            update: Builders<Film>.Update.AddToSet(f => f.Tizers, tizer),
            cancellationToken: token
        )).ModifiedCount > 0;
    public async Task<bool> DeleteTizer(string id, string tizer, CancellationToken token = default) =>
        (await _filmsCol.UpdateOneAsync(
            filter: FilterById(id),
            update: Builders<Film>.Update.Pull(f => f.Tizers, tizer),
            cancellationToken: token
        )).ModifiedCount > 0;
    
    
    public async Task<bool> AddRelatedFilm(string id, string relatedFilmdId, CancellationToken token = default)
    {
        var isExists = (await _filmsCol.FindAsync(
            filter: FilterById(relatedFilmdId), 
            cancellationToken: token)
        )
        .FirstOrDefault() is not null; // find get coll res and i check if any exists in coll
        
        if(!isExists)
            return false;

        var relatedFilmIsAddedToFilm =  (await _filmsCol.UpdateOneAsync(
            filter: FilterById(id),
            update: Builders<Film>.Update.AddToSet(f => f.RelatedFilms, relatedFilmdId),
            cancellationToken: token
        )).ModifiedCount > 0;

        if(!relatedFilmIsAddedToFilm)
            return false;

        return (await _filmsCol.UpdateOneAsync(
            filter: FilterById(relatedFilmdId),
            update: Builders<Film>.Update.AddToSet(f => f.RelatedFilms, id),
            cancellationToken: token
        )).ModifiedCount > 0;
    }
        
    public async Task<bool> DeleteRelatedFilm(string id, string relatedFilmdId, CancellationToken token = default)
    {
        var relatedFilmIsDeletedFromFilm = (await _filmsCol.UpdateOneAsync(
            filter: FilterById(id),
            update: Builders<Film>.Update.Pull(f => f.RelatedFilms, relatedFilmdId),
            cancellationToken: token
        )).ModifiedCount > 0;

        if(!relatedFilmIsDeletedFromFilm)
            return false;

        var filmIsDeletedFromRelatedFilm = (await _filmsCol.UpdateOneAsync(
            filter: FilterById(relatedFilmdId),
            update: Builders<Film>.Update.Pull(f => f.RelatedFilms, id),
            cancellationToken: token
        )).ModifiedCount > 0;

        return filmIsDeletedFromRelatedFilm;
    }
        


    public async Task<bool> AddGenre(string id, string genre, CancellationToken token = default) =>
        (await _filmsCol.UpdateOneAsync(
            filter: FilterById(id),
            update: Builders<Film>.Update.AddToSet(f => f.Genres, genre),
            cancellationToken: token
        )).ModifiedCount > 0;

    public async Task<bool> DeleteGenre(string id, string genre, CancellationToken token = default) =>
        (await _filmsCol.UpdateOneAsync(
            filter: FilterById(id),
            update: Builders<Film>.Update.Pull(f => f.Genres, genre),
            cancellationToken: token)).ModifiedCount > 0;



    public async Task<bool> AddNomination(string id, string nomination, CancellationToken token = default) =>
        (await _filmsCol.UpdateOneAsync(
            filter: FilterById(id),
            update: Builders<Film>.Update.AddToSet(f => f.Nominations, nomination),
            cancellationToken: token
        )).ModifiedCount > 0;

    public async Task<bool> DeleteNomination(string id, string nomination, CancellationToken token = default) =>
        (await _filmsCol.UpdateOneAsync(
            filter: FilterById(id),
            update: Builders<Film>.Update.Pull(f => f.Nominations, nomination),
            cancellationToken: token
        )).ModifiedCount > 0;


    public async Task<bool> UpdateSeasonAndSerias(string id, List<UpdateSeasonDto> newCollectionOfSeasonWithSerias, CancellationToken token = default) => 
        (await _filmsCol.UpdateOneAsync(
            filter: FilterById(id),
            update: BuildUpdateSeasonsDefition(newCollectionOfSeasonWithSerias),
            cancellationToken: token
        )).ModifiedCount > 0;

    UpdateDefinition<Film> BuildUpdateSeasonsDefition(List<UpdateSeasonDto> newCollectionOfSeasonWithSerias) =>
        Builders<Film>.Update.Set(f => f.Seasons, newCollectionOfSeasonWithSerias.Adapt<List<Season>>());
   

    public async Task<bool> UpdateScore(string id, double score, uint scoreCount, CancellationToken token = default) => 
        (await _filmsCol.UpdateOneAsync(
            filter:FilterById(id),
            update: Builders<Film>.Update.Set(f => f.Score, score)
                                         .Set(f => f.ScoreCount, scoreCount),
            cancellationToken: token
        )).ModifiedCount > 0;


    public async Task<bool> UpdateWillWatchCount(string id, uint willWatchCount, CancellationToken token = default) =>
        (await _filmsCol.UpdateOneAsync(
            filter:FilterById(id),
            update: Builders<Film>.Update.Set(f => f.WillWatchCount, willWatchCount),
            cancellationToken: token
        )).ModifiedCount > 0;


    public async Task<bool> UpdateShareCount(string id, uint shareCount, CancellationToken token = default) =>
        (await _filmsCol.UpdateOneAsync(
            filter:FilterById(id),
            update: Builders<Film>.Update.Set(f => f.ShareCount, shareCount),
            cancellationToken: token
        )).ModifiedCount > 0;


    public async Task<bool> UpdateWatchedCount(string id, uint watchedCount, CancellationToken token = default) =>
        (await _filmsCol.UpdateOneAsync(
            filter:FilterById(id),
            update: Builders<Film>.Update.Set(f => f.WatchedCount, watchedCount),
            cancellationToken: token
        )).ModifiedCount > 0;


    public async Task<bool> UpdateViewCount(string id, uint viewCount, CancellationToken token = default) =>
        (await _filmsCol.UpdateOneAsync(
            filter:FilterById(id),
            update: Builders<Film>.Update.Set(f => f.ViewCount, viewCount),
            cancellationToken: token
        )).ModifiedCount > 0;


    public async Task<bool> UpdateNotInterestingCount(string id, uint notInterestingCount, CancellationToken token = default) =>
        (await _filmsCol.UpdateOneAsync(
            filter:FilterById(id),
            update: Builders<Film>.Update.Set(f => f.NotInterestingCount, notInterestingCount),
            cancellationToken: token
        )).ModifiedCount > 0;
}