using Core.Dtos;
using Core.Dtos.UpdateFilmDtos;
using Core.Dtos.UpdateSeasonsDtos;

using Core.Dtos.CreateFilmDtos;

namespace Core.Repositories;

public interface FilmRepository
{
    Task<FilmDto> Create(CreateFilmDto film, CancellationToken token = default);

    Task<IEnumerable<FilmDto>> Get(uint limit = 20, uint page = 1, CancellationToken token = default);

    Task<FilmDto?> Get(string id, CancellationToken token = default);

    Task<FilmDto> UpdateData(string id, UpdateFilmDto film, CancellationToken token = default); 

    Task Delete(string id, CancellationToken token = default);



    Task AddImage(string id, string image, CancellationToken token = default);

    Task DeleteImage(string id, string image, CancellationToken token = default);
    
    
    Task AddPerson(string id, string personId, CancellationToken token = default);

    Task DeletePerson(string id, string personId, CancellationToken token = default);

    
    Task AddArticle(string id, string article, CancellationToken token = default);

    Task DeleteArticle(string id, string article, CancellationToken token = default);
    
    
    Task AddTrailer(string id, string trailer, CancellationToken token = default);

    Task DeleteTrailer(string id, string trailer, CancellationToken token = default);


    Task AddTizer(string id, string tizer, CancellationToken token = default);

    Task DeleteTizer(string id, string tizer, CancellationToken token = default);
    


    Task AddRelatedFilm(string id, string relatedFilmdId, CancellationToken token = default);

    Task DeleteRelatedFilm(string id, string relatedFilmdId, CancellationToken token = default);   
        

    
    Task AddGenre(string id, string genre, CancellationToken token = default);

    Task DeleteGenre(string id, string genre, CancellationToken token = default);


    Task AddNomination(string id, string nomination, CancellationToken token = default);

    Task DeleteNomination(string id, string nomination, CancellationToken token = default);
    

    Task UpdateSeasonAndSerias(string id, List<UpdateSeasonDto> newCollectionOfSeasonWithSerias, CancellationToken token = default);  





    Task UpdateScore(string id, double score, uint scoreCount, CancellationToken token = default);


    Task UpdateWillWatchCount(string id, uint willWatchCount, CancellationToken token = default);


    Task UpdateShareCount(string id, uint shareCount, CancellationToken token = default);


    Task UpdateWatchedCount(string id, uint watchedCount, CancellationToken token = default);


    Task UpdateViewCount(string id, uint viewCount, CancellationToken token = default);


    Task UpdateNotInterestingCount(string id, uint notInterestingCount, CancellationToken token = default);
}
