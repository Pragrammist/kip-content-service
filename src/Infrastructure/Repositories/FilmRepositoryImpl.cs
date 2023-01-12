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
    readonly IMongoCollection<Film> _filmMongoRepo;
    public FilmRepositoryImpl(IMongoCollection<Film> censorMongoRepo)
    {
        _filmMongoRepo = censorMongoRepo;
    }

    public async Task<FilmDto> Create(CreateFilmDto film, CancellationToken token = default) 
    {
        var filmToInsert = film.Adapt<Film>();
        await _filmMongoRepo.InsertOneAsync(filmToInsert);
        return filmToInsert.Adapt<FilmDto>();
    }
        
        

    public async Task<IEnumerable<FilmDto>> Get(uint limit = 20, uint page = 1, CancellationToken token = default) =>
        (await _filmMongoRepo.FindAsync(
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
        (await _filmMongoRepo.FindAsync(
            filter: FilterById(id),
            cancellationToken: token
        )).FirstOrDefault().Adapt<FilmDto>();
    

public async Task<FilmDto> UpdateData(string id, UpdateFilmDto film, CancellationToken token = default) =>
        (await _filmMongoRepo.FindOneAndUpdateAsync(
            filter: FilterById(id),
            update: BuildUpdateDefition(film),
            cancellationToken: token
        )).Adapt<FilmDto>();
    
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
    

    public async Task Delete(string id, CancellationToken token = default) =>
        await _filmMongoRepo.DeleteOneAsync(
            filter: FilterById(id),
            cancellationToken: token
        );

    public async Task AddImage(string id, string image, CancellationToken token = default) =>
        await _filmMongoRepo.UpdateOneAsync(
            filter: FilterById(id),
            update: Builders<Film>.Update.AddToSet(f => f.Images, image),
            cancellationToken: token
        );

    public async Task DeleteImage(string id, string image, CancellationToken token = default) =>
        await _filmMongoRepo.UpdateOneAsync(
            filter: FilterById(id),
            update: Builders<Film>.Update.Pull(f => f.Images, image),
            cancellationToken: token
        );

    public async Task AddPerson(string id, string personId, CancellationToken token = default) =>
        await _filmMongoRepo.UpdateOneAsync(
            filter: FilterById(id),
            update: Builders<Film>.Update.AddToSet(f => f.Stuff, personId),
            cancellationToken: token
        );

    public async Task DeletePerson(string id, string personId, CancellationToken token = default) =>
        await _filmMongoRepo.UpdateOneAsync(
            filter: FilterById(id),
            update: Builders<Film>.Update.Pull(f => f.Stuff, personId),
            cancellationToken: token
        );

    public async Task AddArticle(string id, string article, CancellationToken token = default) =>
        await _filmMongoRepo.UpdateOneAsync(
            filter: FilterById(id),
            update: Builders<Film>.Update.AddToSet(f => f.Articles, article),
            cancellationToken: token
        );

    public async Task DeleteArticle(string id, string article, CancellationToken token = default) =>
        await _filmMongoRepo.UpdateOneAsync(
            filter: FilterById(id),
            update: Builders<Film>.Update.Pull(f => f.Articles, article),
            cancellationToken: token
        );

    public async  Task AddTrailer(string id, string trailer, CancellationToken token = default) =>
        await _filmMongoRepo.UpdateOneAsync(
            filter: FilterById(id),
            update: Builders<Film>.Update.AddToSet(f => f.Trailers, trailer),
            cancellationToken: token
        );

    public async Task DeleteTrailer(string id, string trailer, CancellationToken token = default) =>
        await _filmMongoRepo.UpdateOneAsync(
            filter: FilterById(id),
            update: Builders<Film>.Update.Pull(f => f.Trailers, trailer),
            cancellationToken: token
        );

    public async Task AddTizer(string id, string tizer, CancellationToken token = default) =>
        await _filmMongoRepo.UpdateOneAsync(
            filter: FilterById(id),
            update: Builders<Film>.Update.AddToSet(f => f.Tizers, tizer),
            cancellationToken: token
        );

    public async Task DeleteTizer(string id, string tizer, CancellationToken token = default) =>
        await _filmMongoRepo.UpdateOneAsync(
            filter: FilterById(id),
            update: Builders<Film>.Update.Pull(f => f.Tizers, tizer),
            cancellationToken: token
        );
    public async Task AddRelatedFilm(string id, string relatedFilmdId, CancellationToken token = default) =>
        await _filmMongoRepo.UpdateOneAsync(
            filter: FilterById(id),
            update: Builders<Film>.Update.AddToSet(f => f.RelatedFilms, relatedFilmdId),
            cancellationToken: token
        );

    public async Task DeleteRelatedFilm(string id, string relatedFilmdId, CancellationToken token = default) =>
        await _filmMongoRepo.UpdateOneAsync(
            filter: FilterById(id),
            update: Builders<Film>.Update.Pull(f => f.RelatedFilms, relatedFilmdId),
            cancellationToken: token
        );

    public async Task AddGenre(string id, string genre, CancellationToken token = default) =>
        await _filmMongoRepo.UpdateOneAsync(
            filter: FilterById(id),
            update: Builders<Film>.Update.AddToSet(f => f.Genres, genre),
            cancellationToken: token
        );

    public async Task DeleteGenre(string id, string genre, CancellationToken token = default) =>
        await _filmMongoRepo.UpdateOneAsync(
            filter: FilterById(id),
            update: Builders<Film>.Update.Pull(f => f.Genres, genre),
            cancellationToken: token);

    public async Task AddNomination(string id, string nomination, CancellationToken token = default) =>
        await _filmMongoRepo.UpdateOneAsync(
            filter: FilterById(id),
            update: Builders<Film>.Update.AddToSet(f => f.Nominations, nomination),
            cancellationToken: token
        );

    public async Task DeleteNomination(string id, string nomination, CancellationToken token = default) =>
        await _filmMongoRepo.UpdateOneAsync(
            filter: FilterById(id),
            update: Builders<Film>.Update.Pull(f => f.Nominations, nomination),
            cancellationToken: token
        );

    public async Task UpdateSeasonAndSerias(string id, List<UpdateSeasonDto> newCollectionOfSeasonWithSerias, CancellationToken token = default) => 
        await _filmMongoRepo.UpdateOneAsync(
            filter: FilterById(id),
            update: BuildUpdateSeasonsDefition(newCollectionOfSeasonWithSerias),
            cancellationToken: token
        );

    UpdateDefinition<Film> BuildUpdateSeasonsDefition(List<UpdateSeasonDto> newCollectionOfSeasonWithSerias) =>
        Builders<Film>.Update.Set(f => f.Seasons, newCollectionOfSeasonWithSerias.Adapt<List<Season>>());
   
    public async Task UpdateScore(string id, double score, uint scoreCount, CancellationToken token = default) => 
        await _filmMongoRepo.UpdateOneAsync(
            filter:FilterById(id),
            update: Builders<Film>.Update.Set(f => f.Score, score)
                                         .Set(f => f.ScoreCount, scoreCount),
            cancellationToken: token
        );

    public async Task UpdateWillWatchCount(string id, uint willWatchCount, CancellationToken token = default) =>
        await _filmMongoRepo.UpdateOneAsync(
            filter:FilterById(id),
            update: Builders<Film>.Update.Set(f => f.WillWatchCount, willWatchCount),
            cancellationToken: token
        );

    public async Task UpdateShareCount(string id, uint shareCount, CancellationToken token = default) =>
        await _filmMongoRepo.UpdateOneAsync(
            filter:FilterById(id),
            update: Builders<Film>.Update.Set(f => f.ShareCount, shareCount),
            cancellationToken: token
        );

    public async Task UpdateWatchedCount(string id, uint watchedCount, CancellationToken token = default) =>
        await _filmMongoRepo.UpdateOneAsync(
            filter:FilterById(id),
            update: Builders<Film>.Update.Set(f => f.WatchedCount, watchedCount),
            cancellationToken: token
        );

    public async Task UpdateViewCount(string id, uint viewCount, CancellationToken token = default) =>
        await _filmMongoRepo.UpdateOneAsync(
            filter:FilterById(id),
            update: Builders<Film>.Update.Set(f => f.ViewCount, viewCount),
            cancellationToken: token
        );

    public async Task UpdateNotInterestingCount(string id, uint notInterestingCount, CancellationToken token = default) =>
        await _filmMongoRepo.UpdateOneAsync(
            filter:FilterById(id),
            update: Builders<Film>.Update.Set(f => f.NotInterestingCount, notInterestingCount),
            cancellationToken: token
        );
}