using Xunit;
using FluentAssertions;
using Core;

namespace UnitTests;

public class FilmTest
{
    
    Film GetFilm() => Film.GetTestFilmWithDefaultValue();

    [Fact]
    public void DecrNotInterestingCountOne()
    {
        var film = GetFilm();

        film.IncrNotInterestingCount();

        film.DecrNotInterestingCount();

        film.NotInterestingCount.Should().Be(0);
    }

    [Fact]
    public void DecrNotInterestingCountZero()
    {
        var film = GetFilm();

        film.DecrNotInterestingCount();

        film.NotInterestingCount.Should().Be(0);
    }

    [Fact]
    public void DecrWillWatchCount()
    {
        var film = GetFilm();

        film.IncrWillWatchCount();

        film.DecrWillWatchCount();

        film.WillWatchCount.Should().Be(0);
    }

    [Fact]
    public void DecrWillWatchCountZero()
    {
        var film = GetFilm();

        film.DecrWillWatchCount();

        film.WillWatchCount.Should().Be(0);
    }


    [Fact]
    public void DecrWatchedCount()
    {
        var film = GetFilm();

        film.IncrWatchedCount();

        film.DecrWatchedCount();

        film.WatchedCount.Should().Be(0);
    }

    [Fact]
    public void DecrWatchedCountZero()
    {
        var film = GetFilm();

        film.DecrWatchedCount();

        film.WatchedCount.Should().Be(0);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(6)]
    public void AddScoreNotValidData(uint score)
    {
        var film = GetFilm();

        film.AddScore(score);

        film.ScoreCount.Should().Be(0);

        film.Score.Should().Be(0);
    }


    [Theory]
    [InlineData(1)]
    [InlineData(5)]
    public void AddScoreOneScore(uint score)
    {
        var film = GetFilm();

        film.AddScore(score);

        film.ScoreCount.Should().Be(1);

        film.Score.Should().Be(score);
    }


    [Theory]
    [InlineData (1u,2u,4u,4u,3u,5u)]
    public void AddScoreMultipleScore(params uint[] scores)
    {
        var film = GetFilm();

        foreach(var score in scores)
            film.AddScore(score);

        film.ScoreCount.Should().Be((uint)scores.Length);

        film.Score.Should().BeApproximately(3.17, 0.01);
    }
}
