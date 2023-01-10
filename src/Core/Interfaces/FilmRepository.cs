using Core.Dtos;
using Core.Dtos.UpdateFilmDtos;
using Core.Dtos.UpdateSeasonsDtos;

using Core.Dtos.CreateFilmDtos;

namespace Core.Interfaces;

public interface FilmRepository
{
    void Create(CreateFilmDto film);

    IEnumerable<FilmDto> Get(uint limit = 20, uint page = 1);

    FilmDto Get(string id);

    void UpdateData(UpdateFilmDto film); 

    void Delete(string id);



    void AddImage(string id, string image);

    void DeleteImage(string id, string image);
    
    
    void AddPerson(string id, string personId);

    void DeletePerson(string id, string personId);

    
    void AddArticle(string id, string article);

    void DeleteArticle(string id, string article);
    
    
    void AddTrailer(string id, string trailer);

    void DeleteTrailer(string id, string trailer);


    void AddTizer(string id, string tizer);

    void DeleteTizer(string id, string tizer);
    


    void AddRelatedFilm(string id, string relatedFilmdId);

    void DeleteRelatedFilm(string id, string relatedFilmdId);   
        

    
    void AddGenre(string id, string genre);

    void DeleteGenre(string id, string genre);


    void AddNomination(string id, string nomination);

    void DeleteNomination(string id, string nomination);
    

    public void UpdateSeasonAndSerias(string id, params UpdateSeasonsDto[] newCollectionOfSeasonWithSerias);  





    public void UpdateScore(string id, uint score, uint scoreCount);


    public void UpdateWillWatchCount(string id, uint willWatch);


    public void UpdateShareCount(string id, uint share);


    public void UpdateWatchedCount(string id, uint watched);


    public void UpdateViewCount(string id, uint view);


    public void UpdateNotInterestingCount(string id, uint notInteresting);
}
