using System;
using System.Threading.Tasks;
using Infrastructure.Repositories;
using Xunit;
using Core.Dtos;
using System.Linq;
using Core.Dtos.UpdateFilmDtos;
using Core.Dtos.UpdateSeasonsDtos;
using Infrastructure.Configuration;
using FluentAssertions;
using Core.Dtos.CreateFilmDtos;
using System.Collections.Generic;
using System.IO;

namespace IntegrationTests;

[Collection("MongoDb")]
public class FilmRepositoryIntegrationTest
{
    readonly MongoDbFixture _mongoFixture;
    readonly FilmRepositoryImpl _repo;

    string RandomText => Path.GetRandomFileName();
    
    public FilmRepositoryIntegrationTest(MongoDbFixture mongoFixture)
    {
        MapsterConfiguration.ConfigureMapsterGlobally();
        _mongoFixture = mongoFixture;
        _repo = new FilmRepositoryImpl(_mongoFixture.FilmCollection);
    }
    
    [Fact]
    public async Task AddImage()
    {
        var film = await CreateFilmWithRandomName();

        var res = await _repo.AddImage(film.Id, "someimage");

        res.Should().BeTrue();
    }

    [Fact]
    public async Task DeleteImage()
    {
        var film = await CreateFilmWithRandomName();
        var image = "someimage";
        await _repo.AddImage(film.Id, image);
        
        var res = await _repo.DeleteImage(film.Id, image);

        res.Should().BeTrue();
    }

    [Fact]
    public async Task AddPerson()
    {
        var film = await CreateFilmWithRandomName();

        var res = await _repo.AddPerson(film.Id, "person");

        res.Should().BeTrue();
    }

    [Fact]
    public async Task DeletePerson()
    {
        var film = await CreateFilmWithRandomName();
        var person = "person";
        await _repo.AddPerson(film.Id, person);
        
        var res =  await _repo.DeletePerson(film.Id, person);

        res.Should().BeTrue();
    }
    
    [Fact]
    public async Task AddArticle()
    {
        var film = await CreateFilmWithRandomName();

        var res = await _repo.AddArticle(film.Id, "article");

        res.Should().BeTrue();
    }

    [Fact]
    public async Task DeleteArticle()
    {
        var film = await CreateFilmWithRandomName();
        var article = "article";
        await _repo.AddArticle(film.Id, article);
        
        var res = await _repo.DeleteArticle(film.Id, article);

        res.Should().BeTrue();
    }
    

    [Fact]
    public async Task AddTrailer()
    {
        var film = await CreateFilmWithRandomName();

        var res = await _repo.AddTrailer(film.Id, "Trailer");

        res.Should().BeTrue();
    }

    [Fact]
    public async Task DeleteTrailer()
    {
        var film = await CreateFilmWithRandomName();
        var trailer = "Trailer";
        await _repo.AddTrailer(film.Id, trailer);
        
        var res = await _repo.DeleteTrailer(film.Id, trailer);

        res.Should().BeTrue();
    }
    

    [Fact]
    public async Task AddRelatedFilm()
    {
        var film = await CreateFilmWithRandomName();

        var res = await _repo.AddRelatedFilm(film.Id, "film");

        res.Should().BeTrue();
    }

    [Fact]
    public async Task DeleteRelatedFilm()
    {
        var film = await CreateFilmWithRandomName();
        var relatedFilmdId = "filmdid";
        await _repo.AddRelatedFilm(film.Id, relatedFilmdId);
        
        var res = await _repo.DeleteRelatedFilm(film.Id, relatedFilmdId);

        res.Should().BeTrue();
    }
    


    [Fact]
    public async Task AddGenre()
    {
        var film = await CreateFilmWithRandomName();

        var res = await _repo.AddGenre(film.Id, "genre");

        res.Should().BeTrue();
    }

    [Fact]
    public async Task DeleteGenre()
    {
        var film = await CreateFilmWithRandomName();
        var genre = "genre";
        await _repo.AddGenre(film.Id, genre);
        
        var res = await _repo.DeleteGenre(film.Id, genre);

        res.Should().BeTrue();
    }

    
    [Fact]
    public async Task AddNomination()
    {
        var film = await CreateFilmWithRandomName();

        var res = await _repo.AddNomination(film.Id, "Nomination");

        res.Should().BeTrue();
    }

    [Fact]
    public async Task DeleteNomination()
    {
        var film = await CreateFilmWithRandomName();
        var nomination = "genre";
        await _repo.AddNomination(film.Id, nomination);
        
        var res = await _repo.DeleteNomination(film.Id, nomination);

        res.Should().BeTrue();
    }

    [Fact]
    public async Task UpdateSeasonAndSerias()
    {
        var film = await CreateFilmWithRandomName();
        List<UpdateSeasonDto> seasons = new List<UpdateSeasonDto>(){
            new UpdateSeasonDto { Banner = "bannre", Num = 1, Serias = new List<UpdateSeriaDto> { new UpdateSeriaDto {IdFile = "ew", Num = 1}, new UpdateSeriaDto {IdFile = "ew2", Num = 2}, new UpdateSeriaDto {IdFile = "ew", Num = 1} }},
            new UpdateSeasonDto { Banner = "bannre", Num = 1, Serias = new List<UpdateSeriaDto> { }},
            new UpdateSeasonDto { Banner = "bannre", Num = 1, Serias = new List<UpdateSeriaDto> { }},

        };

        var res = await _repo.UpdateSeasonAndSerias(film.Id, seasons);

        res.Should().BeTrue();
    }


    [Fact]
    public  async Task UpdateShareCount()
    {
        var film = await CreateFilmWithRandomName();

        var res = await _repo.UpdateShareCount(film.Id, 3);

        res.Should().BeTrue();
    }


    [Fact]
    public async Task UpdateWatchedCount()
    {
        var film = await CreateFilmWithRandomName();

        var res = await _repo.UpdateWatchedCount(film.Id, 3);

        res.Should().BeTrue();
    }

    [Fact]
    public async Task UpdateViewCount()
    {
        var film = await CreateFilmWithRandomName();

        var res = await _repo.UpdateViewCount(film.Id, 3);

        res.Should().BeTrue();
    }

    [Fact]
    public async Task UpdateNotInterestingCount()
    {
        var film = await CreateFilmWithRandomName();

        var res = await _repo.UpdateNotInterestingCount(film.Id, 3);

        res.Should().BeTrue();
    }

    [Fact]
    public async Task UpdateScore()
    {
        var film = await CreateFilmWithRandomName();

        var res = await _repo.UpdateScore(film.Id, 5, 1);

        res.Should().BeTrue();
    }

    [Fact]
    public async Task UpdateWillWatchCount()
    {
        var film = await CreateFilmWithRandomName();

        var res = await _repo.UpdateWillWatchCount(film.Id, 3);

        res.Should().BeTrue();
    }

    [Fact]
    public async Task Delete()
    {
        var film = await CreateFilmWithRandomName();

        var res = await _repo.Delete(film.Id);

        res.Should().BeTrue();
    }

    [Fact]
    public async Task Create()
    {
        var film = await CreateFilmWithRandomName();

        film.Should().NotBeNull();
    }

    async Task<FilmDto> CreateFilmWithRandomName()
    {
        var film = new CreateFilmDto 
        {
            AgeLimit = 3,
            Articles = new List<string>(),
            Banner = RandomText,
            Content = RandomText,
            Country = RandomText,
            Description = RandomText,
            Duration = TimeSpan.FromHours(1),
            EndScreening = DateTime.Today,
            Fees = 30,
            Genres = new List<string>(),
            Images = new List<string>(),
            KindOfFilm = 0,
            Name = RandomText,
            Nominations = new List<string>(),
            RelatedFilms = new List<string>(),
            Release = DateTime.Today,
            ReleaseType = 0,
            Seasons = new List<CreateSeasonDto>(),
            StartScreening = DateTime.Today,
            Stuff = new List<string>(),
            Tizers = new List<string>(),
            Trailers = new List<string>()
        };

        return await _repo.Create(film);
    }

    [Fact]
    public async Task Get()
    {
        var film = await CreateFilmWithRandomName();

        var res = await _repo.Get(film.Id);

        res.Should().NotBeNull();
    }

    [Fact]
    public async Task GetMany()
    {
        var film = await CreateFilmWithRandomName();

        var res = await _repo.Get();

        res.Count().Should().BeGreaterThan(0);
    }


    [Fact]
    public async Task UpdateAgeLimit()
    {
        var film = await CreateFilmWithRandomName();
        var updData = new UpdateFilmDto{
            AgeLimit = 1
        };

        var res = await _repo.UpdateData(film.Id, updData);        

        res.Should().BeTrue();
    }


    [Fact]
    public async Task UpdateBanner()
    {
        var film = await CreateFilmWithRandomName();
        var updData = new UpdateFilmDto{
            Banner = RandomText
        };

        var res = await _repo.UpdateData(film.Id, updData);        

        res.Should().BeTrue();
    }


    [Fact]
    public async Task UpdateName()
    {
        var film = await CreateFilmWithRandomName();
        var updData = new UpdateFilmDto{
            Name = RandomText
        };

        var res = await _repo.UpdateData(film.Id, updData);        

        res.Should().BeTrue();
    }


    [Fact]
    public async Task UpdateDescription()
    {
        var film = await CreateFilmWithRandomName();
        var updData = new UpdateFilmDto{
            Description = RandomText
        };

        var res = await _repo.UpdateData(film.Id, updData);        

        res.Should().BeTrue();
    }


    [Fact]
    public async Task UpdateCountry()
    {
        var film = await CreateFilmWithRandomName();
        var updData = new UpdateFilmDto{
            Country = RandomText
        };

        var res = await _repo.UpdateData(film.Id, updData);        

        res.Should().BeTrue();
    }


    [Fact]
    public async Task UpdateKindOfFilm()
    {
        var film = await CreateFilmWithRandomName();
        var updData = new UpdateFilmDto{
            KindOfFilm = 1
        };

        var res = await _repo.UpdateData(film.Id, updData);        

        res.Should().BeTrue();
    }

    [Fact]
    public async Task UpdateReleaseType()
    {
        var film = await CreateFilmWithRandomName();
        var updData = new UpdateFilmDto{
            ReleaseType = 1
        };

        var res = await _repo.UpdateData(film.Id, updData);        

        res.Should().BeTrue();
    }


    [Fact]
    public async Task UpdateDuration()
    {
        var film = await CreateFilmWithRandomName();
        var updData = new UpdateFilmDto{
            Duration = TimeSpan.FromDays(1)
        };

        var res = await _repo.UpdateData(film.Id, updData);        

        res.Should().BeTrue();
    }


    [Fact]
    public async Task UpdateRelease()
    {
        var film = await CreateFilmWithRandomName();
        var updData = new UpdateFilmDto{
            Release = DateTime.MaxValue
        };

        var res = await _repo.UpdateData(film.Id, updData);        

        res.Should().BeTrue();
    }


    [Fact]
    public async Task UpdateStartScreening()
    {
        var film = await CreateFilmWithRandomName();
        var updData = new UpdateFilmDto{
            StartScreening = DateTime.MaxValue
        };

        var res = await _repo.UpdateData(film.Id, updData);        

        res.Should().BeTrue();
    }


    [Fact]
    public async Task UpdateEndScreening()
    {
        var film = await CreateFilmWithRandomName();
        var updData = new UpdateFilmDto{
            EndScreening = DateTime.MaxValue
        };

        var res = await _repo.UpdateData(film.Id, updData);        

        res.Should().BeTrue();
    }

    [Fact]
    public async Task UpdateContent()
    {
        var film = await CreateFilmWithRandomName();
        var updData = new UpdateFilmDto{
            Content = RandomText
        };

        var res = await _repo.UpdateData(film.Id, updData);        

        res.Should().BeTrue();
    }


    [Fact]
    public async Task UpdateFees()
    {
        var film = await CreateFilmWithRandomName();
        var updData = new UpdateFilmDto{
            Fees = 226
        };

        var res = await _repo.UpdateData(film.Id, updData);        

        res.Should().BeTrue();
    }
}
