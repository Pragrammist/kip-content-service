using System.Threading;
using Core.Interactors;
using Xunit;
using Core;
using System.Threading.Tasks;
using Core.Repositories;
using Moq;

namespace UnitTests;

public class FilmInteractorTest
{
    EntityFilmRepository NullGetRepo => GetEntityFilmRepo();
    EntityFilmRepository GetEntityFilmRepo()
    {
        Mock<EntityFilmRepository> fakeRepo = new Mock<EntityFilmRepository>();
        fakeRepo.Setup(f => f.Get(It.IsAny<string>(), It.IsAny<CancellationToken>())).Returns<Film>(null);
        return fakeRepo.Object;
    }
    FilmRepository GetFilmRepo()
    {
        Mock<FilmRepository> fakeRepo = new Mock<FilmRepository>();
        return fakeRepo.Object;
    }
    FilmInteractor Interactor => new FilmInteractor(GetFilmRepo(), NullGetRepo);
    //[Fact]
    public async Task FilmIsNullTest()
    {
        await Interactor.AddScore("", 0);
        
    }
}