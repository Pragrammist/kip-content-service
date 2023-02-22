using System;
using System.IO;
using System.Threading.Tasks;
using Infrastructure.Repositories;
using Xunit;
using Core.Dtos;
using System.Collections.Generic;
using FluentAssertions;
using Core.Interactors;
using Core.Dtos.CreateFilmDtos;

namespace IntegrationTests;

[Collection("MongoDb")]
public class FilmInteractorIntegrationTest
{
    readonly MongoDbFixture _mongoFixture;
    readonly FilmRepositoryImpl _repo;

    string RandomText => Path.GetRandomFileName();
    readonly FilmInteractor _filmInteractor;
    readonly EntityFilmRepositoryImpl _entityRepo;
    public FilmInteractorIntegrationTest(MongoDbFixture mongoFixture)
    {
        _mongoFixture = mongoFixture;
        _repo = new FilmRepositoryImpl(_mongoFixture.FilmCollection, _mongoFixture.PersonCollection);

        _entityRepo = new EntityFilmRepositoryImpl(_mongoFixture.FilmCollection);
        _filmInteractor = new FilmInteractor(_repo, _entityRepo);
    }

    
    [Fact]
    public async Task AddScore()
    {
        var film = await CreateFilmWithRandomName();

        var res = await _filmInteractor.AddScore(film.Id, 5);

        res.Should().BeTrue();
    }

    [Fact]
    public async Task IncrWillWatchCount()
    {
        var film = await CreateFilmWithRandomName();

        var res = await _filmInteractor.IncrWillWatchCount(film.Id);

        res.Should().BeTrue();
    }

    [Fact]
    public async Task DecrWillWatchCount()
    {
        var film = await CreateFilmWithRandomName();
        await _filmInteractor.IncrWillWatchCount(film.Id);

        var res = await _filmInteractor.DecrWillWatchCount(film.Id);

        res.Should().BeTrue();
    }

    [Fact]
    public async Task IncrShareCount()
    {
        var film = await CreateFilmWithRandomName();

        var res = await _filmInteractor.IncrShareCount(film.Id);

        res.Should().BeTrue();
    }

    [Fact]
    public async Task IncrViewsCount()
    {
        var film = await CreateFilmWithRandomName();

        var res = await _filmInteractor.IncrViewsCount(film.Id);

        res.Should().BeTrue();
    }

    [Fact]
    public async Task IncrNotInterestingCount()
    {
        var film = await CreateFilmWithRandomName();

        var res = await _filmInteractor.IncrNotInterestingCount(film.Id);

        res.Should().BeTrue();
    }

    [Fact]
    public async Task DecrNotInterestingCount()
    {
        var film = await CreateFilmWithRandomName();
        await _filmInteractor.IncrNotInterestingCount(film.Id);

        var res = await _filmInteractor.DecrNotInterestingCount(film.Id);

        res.Should().BeTrue();
    }

    [Fact]
    public async Task IncrWatchedCount()
    {
        var film = await CreateFilmWithRandomName();

        var res = await _filmInteractor.IncrWatchedCount(film.Id);

        res.Should().BeTrue();
    }

    [Fact]
    public async Task DecrWatchedCount()
    {
        var film = await CreateFilmWithRandomName();
        await _filmInteractor.IncrWatchedCount(film.Id);

        var res = await _filmInteractor.DecrWatchedCount(film.Id);

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

        return await _repo.Create(film);
    }
}