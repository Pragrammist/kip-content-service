using Core.Dtos;
using Core.Dtos.UpdateFilmDtos;
using Core.Dtos.UpdateSeasonsDtos;

using Core.Dtos.CreateFilmDtos;

namespace Core.Repositories;

public interface FilmRepository
{
    Task<FilmDto> Create(CreateFilmDto film, CancellationToken token = default);

    Task<IEnumerable<FilmDto>> Get(int limit, int skip, CancellationToken token = default);

    Task<FilmDto?> Get(string id, CancellationToken token = default);

    Task<bool> UpdateData(string id, UpdateFilmDto film, CancellationToken token = default); 

    Task<bool> Delete(string id, CancellationToken token = default);



    Task<bool> AddImage(string id, string image, CancellationToken token = default);

    Task<bool> DeleteImage(string id, string image, CancellationToken token = default);
    
    
    Task<bool> AddPerson(string id, string personId, CancellationToken token = default);

    Task<bool> DeletePerson(string id, string personId, CancellationToken token = default);

    
    Task<bool> AddArticle(string id, string article, CancellationToken token = default);

    Task<bool> DeleteArticle(string id, string article, CancellationToken token = default);
    
    
    Task<bool> AddTrailer(string id, string trailer, CancellationToken token = default);

    Task<bool> DeleteTrailer(string id, string trailer, CancellationToken token = default);


    Task<bool> AddTizer(string id, string tizer, CancellationToken token = default);

    Task<bool> DeleteTizer(string id, string tizer, CancellationToken token = default);
    


    Task<bool> AddRelatedFilm(string id, string relatedFilmdId, CancellationToken token = default);

    Task<bool> DeleteRelatedFilm(string id, string relatedFilmdId, CancellationToken token = default);   
        

    
    Task<bool> AddGenre(string id, string genre, CancellationToken token = default);

    Task<bool> DeleteGenre(string id, string genre, CancellationToken token = default);


    Task<bool> AddNomination(string id, string nomination, CancellationToken token = default);

    Task<bool> DeleteNomination(string id, string nomination, CancellationToken token = default);
    

    Task<bool> UpdateSeasonAndSerias(string id, List<UpdateSeasonDto> newCollectionOfSeasonWithSerias, CancellationToken token = default);  





    Task<bool> UpdateScore(string id, double score, uint scoreCount, CancellationToken token = default);


    Task<bool> UpdateWillWatchCount(string id, uint willWatchCount, CancellationToken token = default);


    Task<bool> UpdateShareCount(string id, uint shareCount, CancellationToken token = default);


    Task<bool> UpdateWatchedCount(string id, uint watchedCount, CancellationToken token = default);


    Task<bool> UpdateViewCount(string id, uint viewCount, CancellationToken token = default);


    Task<bool> UpdateNotInterestingCount(string id, uint notInterestingCount, CancellationToken token = default);
}
