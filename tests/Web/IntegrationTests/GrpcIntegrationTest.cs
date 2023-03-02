using System;
using Xunit;
using Web;

using GrpcFilmService;
using System.Threading.Tasks;
using System.IO;
using System.Collections.Generic;
using Core.Dtos.CreateFilmDtos;
using Core.Dtos;
using FluentAssertions;

namespace IntegrationTests;

[Collection("WebContext")]
public class GrpcIntegrationTest
{
    readonly WebFixture _webFixture;
    string RandomText => Path.GetRandomFileName();
    public GrpcIntegrationTest (WebFixture webFixture)
    {
        _webFixture = webFixture;
    }
    [Fact]
    public async Task IncrShareCountAsync()
    {
        var filmReq = await CreateFilmWithRandomNameAsRequest();

        var res = await _webFixture.GrpcClient.IncrShareCountAsync(filmReq);
    }

    [Fact]
    public async Task IncrNotInterestingCountAsync()
    {
        var filmReq = await CreateFilmWithRandomNameAsRequest();

        var res = await _webFixture.GrpcClient.IncrNotInterestingCountAsync(filmReq);
    }

    [Fact]
    public async Task DecrNotInterestingCountAsync()
    {
        var filmReq = await CreateFilmWithRandomNameAsRequest();

        var res = await _webFixture.GrpcClient.DecrNotInterestingCountAsync(filmReq);
    }

    [Fact]
    public async Task IncrViewsCountAsync()
    {
        var filmReq = await CreateFilmWithRandomNameAsRequest();

        var res = await _webFixture.GrpcClient.IncrViewsCountAsync(filmReq);
    }

    [Fact]
    public async Task IncrWatchedCountAsync()
    {
        var filmReq = await CreateFilmWithRandomNameAsRequest();

        var res = await _webFixture.GrpcClient.IncrWatchedCountAsync(filmReq);
    }

    [Fact]
    public async Task DecrWatchedCountAsync()
    {
        var filmReq = await CreateFilmWithRandomNameAsRequest();

        var res = await _webFixture.GrpcClient.DecrWatchedCountAsync(filmReq);
    }

    [Fact]
    public async Task IncrWillWatchCountAsync()
    {
        var filmReq = await CreateFilmWithRandomNameAsRequest();

        var res = await _webFixture.GrpcClient.IncrWillWatchCountAsync(filmReq);
    }

    [Fact]
    public async Task DecrWillWatchCountAsync()
    {
        var filmReq = await CreateFilmWithRandomNameAsRequest();

        var res = await _webFixture.GrpcClient.DecrWillWatchCountAsync(filmReq);
    }

    [Fact]
    public async Task ScoreAsync()
    {
        var filmReq = await CreateFilmWithRandomNameAsRequest();
        ScoreRequest scoreRequest = new ScoreRequest {FilmdId = filmReq.FilmdId, Score = 3};

        var res = await _webFixture.GrpcClient.ScoreAsync(scoreRequest);
    }

    async Task<FilmIdRequest> CreateFilmWithRandomNameAsRequest()
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
        var createdFilm = await _webFixture.FilmRepository.Create(film);
        FilmIdRequest request = new FilmIdRequest{
            FilmdId = createdFilm.Id
        };
        return request;
    }

    
}
