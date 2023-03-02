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
using Core.Dtos.CreatePersonDtos;
using Core;
using MongoDB.Bson;

namespace IntegrationTests;

[Collection("MongoDb")]
public class FilmRepositoryIntegrationTest
{
    readonly MongoDbFixture _mongoFixture;
    readonly FilmRepositoryImpl _filmRepo;

    readonly PersonRepositoryImpl _personRepo;

    readonly FilmSelectionRepositoryImpl _selectionRepo;

    readonly CensorRepositoryImpl _censorRepo;

    string RandomText => Path.GetRandomFileName();
    
    public FilmRepositoryIntegrationTest(MongoDbFixture mongoFixture)
    {
        _mongoFixture = mongoFixture;
        _filmRepo = new FilmRepositoryImpl(_mongoFixture.FilmCollection, _mongoFixture.PersonCollection, _mongoFixture.CensorCollection, _mongoFixture.FilmSelectionCollection);
        _selectionRepo = new FilmSelectionRepositoryImpl(_mongoFixture.FilmSelectionCollection, _mongoFixture.FilmCollection);
        _personRepo = new PersonRepositoryImpl(_mongoFixture.PersonCollection, _mongoFixture.FilmCollection);
        _censorRepo = new CensorRepositoryImpl(_mongoFixture.CensorCollection, _mongoFixture.FilmCollection);
    }
    
    [Fact]
    public async Task AddImage()
    {
        var film = await CreateFilmWithRandomName();

        var res = await _filmRepo.AddImage(film.Id, "someimage");

        res.Should().BeTrue();
    }

    [Fact]
    public async Task DeleteImage()
    {
        var film = await CreateFilmWithRandomName();
        var image = "someimage";
        await _filmRepo.AddImage(film.Id, image);
        
        var res = await _filmRepo.DeleteImage(film.Id, image);

        res.Should().BeTrue();
    }

    [Fact]
    public async Task AddPerson()
    {
        var film = await CreateFilmWithRandomName();
        var personId = await CreatePerson();

        var res = await _filmRepo.AddPerson(film.Id, personId);

        res.Should().BeTrue();
    }

    [Fact]
    public async Task AddNotExistsPerson()
    {
        var film = await CreateFilmWithRandomName();
        var personId = ObjectId.GenerateNewId().ToString();

        var res = await _filmRepo.AddPerson(film.Id, personId);

        res.Should().BeFalse();
    }

    [Fact]
    public async Task DeletePerson()
    {
        var film = await CreateFilmWithRandomName();
        var personId = await CreatePerson();
        var personIsAdded = await _filmRepo.AddPerson(film.Id, personId);
        
        var res =  await _filmRepo.DeletePerson(film.Id, personId);

        res.Should().BeTrue();
    }
    
    [Fact]
    public async Task AddArticle()
    {
        var film = await CreateFilmWithRandomName();

        var res = await _filmRepo.AddArticle(film.Id, "article");

        res.Should().BeTrue();
    }

    [Fact]
    public async Task DeleteArticle()
    {
        var film = await CreateFilmWithRandomName();
        var article = "article";
        await _filmRepo.AddArticle(film.Id, article);
        
        var res = await _filmRepo.DeleteArticle(film.Id, article);

        res.Should().BeTrue();
    }
    

    [Fact]
    public async Task AddTrailer()
    {
        var film = await CreateFilmWithRandomName();

        var res = await _filmRepo.AddTrailer(film.Id, "Trailer");

        res.Should().BeTrue();
    }

    [Fact]
    public async Task DeleteTrailer()
    {
        var film = await CreateFilmWithRandomName();
        var trailer = "Trailer";
        await _filmRepo.AddTrailer(film.Id, trailer);
        
        var res = await _filmRepo.DeleteTrailer(film.Id, trailer);

        res.Should().BeTrue();
    }
    

    [Fact]
    public async Task AddRelatedFilm()
    {
        var film = await CreateFilmWithRandomName();
        var relatedFilm = await CreateFilmWithRandomName();

        var res = await _filmRepo.AddRelatedFilm(film.Id, relatedFilm.Id);

        res.Should().BeTrue();
    }

    [Fact]
    public async Task AddNotExistsRelatedFilm()
    {
        var film = await CreateFilmWithRandomName();
        var relatedFilm = ObjectId.GenerateNewId().ToString();

        var res = await _filmRepo.AddRelatedFilm(film.Id, relatedFilm);

        res.Should().BeFalse();
    }

    [Fact]
    public async Task DeleteRelatedFilm()
    {
        var film = await CreateFilmWithRandomName();
        var relatedFilmdId = (await CreateFilmWithRandomName()).Id;
        var filmAdded = await _filmRepo.AddRelatedFilm(film.Id, relatedFilmdId);
        
        var res = await _filmRepo.DeleteRelatedFilm(film.Id, relatedFilmdId);

        res.Should().BeTrue();
    }
    


    [Fact]
    public async Task AddGenre()
    {
        var film = await CreateFilmWithRandomName();

        var res = await _filmRepo.AddGenre(film.Id, "genre");

        res.Should().BeTrue();
    }

    [Fact]
    public async Task DeleteGenre()
    {
        var film = await CreateFilmWithRandomName();
        var genre = "genre";
        await _filmRepo.AddGenre(film.Id, genre);
        
        var res = await _filmRepo.DeleteGenre(film.Id, genre);

        res.Should().BeTrue();
    }

    
    [Fact]
    public async Task AddNomination()
    {
        var film = await CreateFilmWithRandomName();

        var res = await _filmRepo.AddNomination(film.Id, "Nomination");

        res.Should().BeTrue();
    }

    [Fact]
    public async Task DeleteNomination()
    {
        var film = await CreateFilmWithRandomName();
        var nomination = "genre";
        await _filmRepo.AddNomination(film.Id, nomination);
        
        var res = await _filmRepo.DeleteNomination(film.Id, nomination);

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

        var res = await _filmRepo.UpdateSeasonAndSerias(film.Id, seasons);

        res.Should().BeTrue();
    }


    [Fact]
    public  async Task UpdateShareCount()
    {
        var film = await CreateFilmWithRandomName();

        var res = await _filmRepo.UpdateShareCount(film.Id, 3);

        res.Should().BeTrue();
    }


    [Fact]
    public async Task UpdateWatchedCount()
    {
        var film = await CreateFilmWithRandomName();

        var res = await _filmRepo.UpdateWatchedCount(film.Id, 3);

        res.Should().BeTrue();
    }

    [Fact]
    public async Task UpdateViewCount()
    {
        var film = await CreateFilmWithRandomName();

        var res = await _filmRepo.UpdateViewCount(film.Id, 3);

        res.Should().BeTrue();
    }

    [Fact]
    public async Task UpdateNotInterestingCount()
    {
        var film = await CreateFilmWithRandomName();

        var res = await _filmRepo.UpdateNotInterestingCount(film.Id, 3);

        res.Should().BeTrue();
    }

    [Fact]
    public async Task UpdateScore()
    {
        var film = await CreateFilmWithRandomName();

        var res = await _filmRepo.UpdateScore(film.Id, 5, 1);

        res.Should().BeTrue();
    }

    [Fact]
    public async Task UpdateWillWatchCount()
    {
        var film = await CreateFilmWithRandomName();

        var res = await _filmRepo.UpdateWillWatchCount(film.Id, 3);

        res.Should().BeTrue();
    }

    [Fact]
    public async Task Delete()
    {
        var film = await CreateFilmWithRandomName();

        var res = await _filmRepo.Delete(film.Id);

        res.Should().BeTrue();
    }
    [Fact]
    public async Task DeleteCheckRelatedFilm()
    {
        var film = await CreateFilmWithRandomName();
        var relatedFilm = await CreateFilmWithRandomName();
        await _filmRepo.AddRelatedFilm(film.Id, relatedFilm.Id);

        var res = await _filmRepo.Delete(film.Id);
        
        var filmFromDb = await _filmRepo.Get(relatedFilm.Id) ?? throw new NullReferenceException("gotten film is null after updating");
        var relatedFilmIsDeletedFromFilm = !filmFromDb.RelatedFilms.Contains(film.Id);

        relatedFilmIsDeletedFromFilm.Should().BeTrue();
        res.Should().BeTrue();
    }

    [Fact]
    public async Task DeleteCheckPerson()
    {
        var film = await CreateFilmWithRandomName();
        var personId = await CreatePerson();
        await _filmRepo.AddPerson(film.Id, personId);

        var res = await _filmRepo.Delete(film.Id);
        
        var person = await _personRepo.Get(personId) ?? throw new NullReferenceException("gotten person is null after updating");
        var personIsNotContainsFilm = !person.Films.Contains(film.Id);

        personIsNotContainsFilm.Should().BeTrue();
        res.Should().BeTrue();
    }

    [Fact]
    public async Task DeleteCheckCensor()
    {
        var film = await CreateFilmWithRandomName();
        var censor = await _censorRepo.Create(RandomText, films: new List<string> { film.Id }) ?? throw new NullReferenceException("gotten censor is null when creating");
        

        var res = await _filmRepo.Delete(film.Id);
        
        var updatedCensor = await _censorRepo.Get(censor.Id) ?? throw new NullReferenceException("gotten person is null after updating");
        var personIsNotContainsFilm = !updatedCensor.Films.Contains(film.Id);

        personIsNotContainsFilm.Should().BeTrue();
        res.Should().BeTrue();
    }

    [Fact]
    public async Task DeleteCheckSelection()
    {
        var film = await CreateFilmWithRandomName();
        var selection = await _selectionRepo.Create(RandomText, films: new List<string> { film.Id }) ?? throw new NullReferenceException("gotten selection is null when creating");
        

        var res = await _filmRepo.Delete(film.Id);
        
        var updatedSelection = await _selectionRepo.Get(selection.Id) ?? throw new NullReferenceException("gotten person is null after updating");
        var personIsNotContainsFilm = !updatedSelection.Films.Contains(film.Id);

        personIsNotContainsFilm.Should().BeTrue();
        res.Should().BeTrue();
    }

    [Fact]
    public async Task Create()
    {
        var film = await CreateFilmWithRandomName();

        film.Should().NotBeNull();
    }

    

    [Fact]
    public async Task Get()
    {
        var film = await CreateFilmWithRandomName();

        var res = await _filmRepo.Get(film.Id);

        res.Should().NotBeNull();
    }

    [Fact]
    public async Task GetMany()
    {
        var film = await CreateFilmWithRandomName();

        var res = await _filmRepo.Get();

        res.Count().Should().BeGreaterThan(0);
    }


    [Fact]
    public async Task UpdateAgeLimit()
    {
        var film = await CreateFilmWithRandomName();
        var updData = new UpdateFilmDto{
            AgeLimit = 1
        };

        var res = await _filmRepo.UpdateData(film.Id, updData);        

        res.Should().BeTrue();
    }


    [Fact]
    public async Task UpdateBanner()
    {
        var film = await CreateFilmWithRandomName();
        var updData = new UpdateFilmDto{
            Banner = RandomText
        };

        var res = await _filmRepo.UpdateData(film.Id, updData);        

        res.Should().BeTrue();
    }


    [Fact]
    public async Task UpdateName()
    {
        var film = await CreateFilmWithRandomName();
        var updData = new UpdateFilmDto{
            Name = RandomText
        };

        var res = await _filmRepo.UpdateData(film.Id, updData);        

        res.Should().BeTrue();
    }


    [Fact]
    public async Task UpdateDescription()
    {
        var film = await CreateFilmWithRandomName();
        var updData = new UpdateFilmDto{
            Description = RandomText
        };

        var res = await _filmRepo.UpdateData(film.Id, updData);        

        res.Should().BeTrue();
    }


    [Fact]
    public async Task UpdateCountry()
    {
        var film = await CreateFilmWithRandomName();
        var updData = new UpdateFilmDto{
            Country = RandomText
        };

        var res = await _filmRepo.UpdateData(film.Id, updData);        

        res.Should().BeTrue();
    }


    [Fact]
    public async Task UpdateKindOfFilm()
    {
        var film = await CreateFilmWithRandomName();
        var updData = new UpdateFilmDto{
            KindOfFilm = 1
        };

        var res = await _filmRepo.UpdateData(film.Id, updData);        

        res.Should().BeTrue();
    }

    [Fact]
    public async Task UpdateReleaseType()
    {
        var film = await CreateFilmWithRandomName();
        var updData = new UpdateFilmDto{
            ReleaseType = 1
        };

        var res = await _filmRepo.UpdateData(film.Id, updData);        

        res.Should().BeTrue();
    }


    [Fact]
    public async Task UpdateDuration()
    {
        var film = await CreateFilmWithRandomName();
        var updData = new UpdateFilmDto{
            Duration = TimeSpan.FromDays(1)
        };

        var res = await _filmRepo.UpdateData(film.Id, updData);        

        res.Should().BeTrue();
    }


    [Fact]
    public async Task UpdateRelease()
    {
        var film = await CreateFilmWithRandomName();
        var updData = new UpdateFilmDto{
            Release = DateTime.MaxValue
        };

        var res = await _filmRepo.UpdateData(film.Id, updData);        

        res.Should().BeTrue();
    }


    [Fact]
    public async Task UpdateStartScreening()
    {
        var film = await CreateFilmWithRandomName();
        var updData = new UpdateFilmDto{
            StartScreening = DateTime.MaxValue
        };

        var res = await _filmRepo.UpdateData(film.Id, updData);        

        res.Should().BeTrue();
    }


    [Fact]
    public async Task UpdateEndScreening()
    {
        var film = await CreateFilmWithRandomName();
        var updData = new UpdateFilmDto{
            EndScreening = DateTime.MaxValue
        };

        var res = await _filmRepo.UpdateData(film.Id, updData);        

        res.Should().BeTrue();
    }

    [Fact]
    public async Task UpdateContent()
    {
        var film = await CreateFilmWithRandomName();
        var updData = new UpdateFilmDto{
            Content = RandomText
        };

        var res = await _filmRepo.UpdateData(film.Id, updData);        

        res.Should().BeTrue();
    }


    [Fact]
    public async Task UpdateFees()
    {
        var film = await CreateFilmWithRandomName();
        var updData = new UpdateFilmDto{
            Fees = 226
        };

        var res = await _filmRepo.UpdateData(film.Id, updData);        

        res.Should().BeTrue();
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

        return await _filmRepo.Create(film);
    }

    async Task <string> CreatePerson()
    {
        Person p = new Person(RandomText, DateTime.Now, RandomText, RandomText, RandomText, 180);
        await _mongoFixture.PersonCollection.InsertOneAsync(p);
        return p.Id;
    }
}
