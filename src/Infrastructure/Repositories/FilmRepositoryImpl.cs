using System.Collections.Immutable;
using System.Security.Cryptography.X509Certificates;
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
    readonly IMongoCollection<Person> _personsCol;
    readonly IMongoCollection<FilmSelection> _selectionsCol;
    readonly IMongoCollection<Censor> _censorsCol;
    public FilmRepositoryImpl(
        IMongoCollection<Film> filmCol, 
        IMongoCollection<Person> personsCol,
        IMongoCollection<Censor> censorsCol,
        IMongoCollection<FilmSelection> selectionsCol
    )
    {
        _filmsCol = filmCol;
        _personsCol = personsCol;
        _selectionsCol = selectionsCol;
        _censorsCol = censorsCol;
    }

    public async Task<FilmDto> Create(CreateFilmDto film, CancellationToken token = default) 
    {
        await CheckLists(film);
        var filmToInsert = film.Adapt<Film>();
        await _filmsCol.InsertOneAsync(filmToInsert);
        return filmToInsert.Adapt<FilmDto>();
    }
    async Task CheckLists(CreateFilmDto film)
    {
        var message = "";
        var personCheck = await CheckPersons(film);

        if(personCheck is not null)
            message += personCheck + "\n";

        var filmCheck = await CheckFilms(film);

        if(filmCheck is not null)
            message += filmCheck + "\n";

        if(filmCheck is not null || personCheck is not null)
            throw new FilmNotValidException(message);

    }
    
    async Task<string?> CheckPersons(CreateFilmDto film)
    {
        var notValidIds = new List<string>();
        foreach(var personId in film.Stuff)
        {
            var persons = await _personsCol.FindAsync(filter: PersonFilterId(personId));
            var person = await persons.FirstOrDefaultAsync();
            if(person is null)
                notValidIds.Add(personId);                
        }
        return notValidIds.Count > 0 ? NotValidIdsMessage(notValidIds) : null;
    }

    async Task<string?> CheckPersons(List<string> inpPersons)
    {
        var notValidIds = new List<string>();
        foreach(var personId in inpPersons)
        {
            var persons = await _personsCol.FindAsync(filter: PersonFilterId(personId));
            var person = await persons.FirstOrDefaultAsync();
            if(person is null)
                notValidIds.Add(personId);                
        }
        return notValidIds.Count > 0 ? NotValidIdsMessage(notValidIds) : null;
    }

    async Task<string?> CheckFilms(List<string> inpFilms)
    {
        var notValidIds = new List<string>();
        foreach(var filmId in inpFilms)
        {
            var films = await _filmsCol.FindAsync(filter: FilterById(filmId));
            var relatedFilm = await films.FirstOrDefaultAsync();
            if(relatedFilm is null)
                notValidIds.Add(filmId);                
        }
        return notValidIds.Count > 0 ? NotValidIdsMessage(notValidIds) : null;
    }

    async Task<string?> CheckFilms(CreateFilmDto film)
    {
        var notValidIds = new List<string>();
        foreach(var filmId in film.RelatedFilms)
        {
            var films = await _filmsCol.FindAsync(filter: FilterById(filmId));
            var relatedFilm = await films.FirstOrDefaultAsync();
            if(relatedFilm is null)
                notValidIds.Add(filmId);                
        }
        return notValidIds.Count > 0 ? NotValidIdsMessage(notValidIds) : null;
    }
    string NotValidIdsMessage(List<string> notValidIds)
    {
        var aggIds = notValidIds.Aggregate(((id1, id2) => $"{id1}, {id2}"));
        return $"Person ids {aggIds} not valid";
    }

    public async Task<IEnumerable<FilmDto>> Get(int limit, int skip, CancellationToken token = default) =>
        (await _filmsCol.FindAsync(
            filter: Builders<Film>.Filter.Empty,
            options: new FindOptions<Film>{
                Limit = limit,
                Skip = skip
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
            update: await BuildUpdateDefition(film),
            cancellationToken: token
        )).ModifiedCount > 0;
    

    async Task<UpdateDefinition<Film>> BuildUpdateDefition(UpdateFilmDto film)
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
        
        if(film.Images is not null)
            updList.Add(Builders<Film>.Update.Set(f => f.Images, film.Images));

        if(film.Persons is not null)
        {
            var checkRes = await CheckPersons(film.Persons);

            if(checkRes is not null)
                throw new InvalidOperationException(checkRes);
            
            updList.Add(Builders<Film>.Update.Set(f => f.Stuff, film.Persons));
            
        }

        if(film.RelatedFilms is not null)
        {
            var checkRes = await CheckFilms(film.RelatedFilms);
            
            if(checkRes is not null)
                throw new InvalidOperationException(checkRes);
            
            updList.Add(Builders<Film>.Update.Set(f => f.RelatedFilms, film.RelatedFilms));
            
        }

        if(film.Articles is not null)
            updList.Add(Builders<Film>.Update.Set(f => f.Articles, film.Articles));
        

        if(film.Trailers is not null)
            updList.Add(Builders<Film>.Update.Set(f => f.Trailers, film.Trailers));


        if(film.Tizers is not null)
            updList.Add(Builders<Film>.Update.Set(f => f.Tizers, film.Tizers));


        if(film.Nominations is not null)
            updList.Add(Builders<Film>.Update.Set(f => f.Nominations, film.Nominations));


        if(film.Genres is not null)
            updList.Add(Builders<Film>.Update.Set(f => f.Genres, film.Genres));
            
        UpdateDefinition<Film> finalUpdate = Builders<Film>.Update.Combine(updList);
        return finalUpdate;
    }
    

    public async Task<bool> Delete(string id, CancellationToken token = default) 
    {
        var isDeleted = (await _filmsCol.DeleteOneAsync(
            filter: FilterById(id),
            cancellationToken: token
        )).DeletedCount > 0;

        if(!isDeleted)
            return false;
        
        var isAllCleared = await ClearReferencesWhenDelete(id, token);

        return isDeleted && isAllCleared;
    }
        
    async Task<bool> ClearReferencesWhenDelete(string id, CancellationToken token)
    {
        var deleteFromRelatedFilmRes = await _filmsCol.UpdateManyAsync(
            filter: Builders<Film>.Filter.AnyEq(f => f.RelatedFilms, id),
            update: Builders<Film>.Update.Pull(f => f.RelatedFilms, id),
            cancellationToken: token
        );

        var filmIsDeletedFromRelatedFilm = deleteFromRelatedFilmRes.ModifiedCount > 0 && 
            deleteFromRelatedFilmRes.MatchedCount > 0  || 
            deleteFromRelatedFilmRes.MatchedCount == 0;

        var deletedFromCensorRes = await _censorsCol.UpdateManyAsync(
            filter: Builders<Censor>.Filter.AnyEq(f => f.Films, id),
            update: Builders<Censor>.Update.Pull(f => f.Films, id),
            cancellationToken: token
        );
        var filmIsDeletedFromCensor = deletedFromCensorRes.ModifiedCount > 0 && 
            deletedFromCensorRes.MatchedCount > 0  || 
            deletedFromCensorRes.MatchedCount == 0;

        var deletedFromSelectionRes = await _selectionsCol.UpdateManyAsync(
            filter: Builders<FilmSelection>.Filter.AnyEq(f => f.Films, id),
            update: Builders<FilmSelection>.Update.Pull(f => f.Films, id),
            cancellationToken: token
        );
        var filmIsDeletedFromSelection = deletedFromSelectionRes.ModifiedCount > 0 && 
            deletedFromSelectionRes.MatchedCount > 0  || 
            deletedFromSelectionRes.MatchedCount == 0;

        var deletedFromPersonRes = await _personsCol.UpdateManyAsync(
            filter: Builders<Person>.Filter.AnyEq(f => f.Films, id),
            update: Builders<Person>.Update.Pull(f => f.Films, id),
            cancellationToken: token
        );
        var filmIsDeletedFromPerson = deletedFromPersonRes.ModifiedCount > 0 && 
            deletedFromPersonRes.MatchedCount > 0  || 
            deletedFromPersonRes.MatchedCount == 0;

        return filmIsDeletedFromRelatedFilm || 
                filmIsDeletedFromCensor ||
                filmIsDeletedFromSelection ||
                filmIsDeletedFromPerson;
    }

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