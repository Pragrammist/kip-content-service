using System.IO;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Xunit;

namespace IntegrationTests;

[Collection("WebContext")]
public class WebFilmIntegrationTest
{
    readonly WebFixture _webFixture;
    string RandomText => Path.GetRandomFileName();
    public WebFilmIntegrationTest (WebFixture webFixture)
    {
        _webFixture = webFixture;
    }

    [Fact]
    public async Task Create()
    {
        var film = new 
        {
            AgeLimit = 18,
            Banner = RandomText,
            Name = RandomText,
            Description = RandomText,
            Country = RandomText,
            KindOfFilm = 0,
            ReleaseType = 0,
            Duration = "00:01:01",
            Release = "2023-02-25",
            StartScreening = "2023-02-25",
            EndScreening = "2023-02-25",
            Content = RandomText,
            Fees = 10,
            Images = new [] {"image"},
            Articles = new [] {"article"},
            Trailers = new [] {"trailer"},
            Tizers = new [] {"tizer"},
            Genres = new [] {RandomText},
            Nominations = new [] {RandomText}, 
        };
        var res =  await _webFixture.HttpClient.PostAsJsonAsync("film", film);

        res.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task Delete()
    {
        var filmId = await CreateFilm();

        var res =  await _webFixture.HttpClient.DeleteAsync($"film/{filmId}");

        res.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task GetOne()
    {
        var filmId = await CreateFilm();

        var res =  await _webFixture.HttpClient.GetAsync($"film/{filmId}");

        res.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task GetMany()
    {
        var filmId = await CreateFilm();

        var res =  await _webFixture.HttpClient.GetAsync($"films");

        res.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task ChangeInfo()
    {
        var filmId = await CreateFilm();
        var newInfo = new {
            Name = RandomText
        };
        var res =  await _webFixture.HttpClient.PutAsJsonAsync($"film/{filmId}", newInfo);

        res.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task IncrViews()
    {
        var filmId = await CreateFilm();
        
        var res =  await _webFixture.HttpClient.PutAsJsonAsync($"film/view/{filmId}", new{});

        res.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task IncrShare()
    {
        var filmId = await CreateFilm();
        
        var res =  await _webFixture.HttpClient.PutAsJsonAsync($"film/share/{filmId}", new{});

        res.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task AddImage()
    {
        var filmId = await CreateFilm();
        var image = RandomText;

        var res =  await _webFixture.HttpClient.PutAsJsonAsync($"film/images/{filmId}/{image}", new{});

        res.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task DeleteImage()
    {
        var filmId = await CreateFilm();
        var image = RandomText;
        var resOfAdding =  await _webFixture.HttpClient.PutAsJsonAsync($"film/images/{filmId}/{image}", new{});

        var res = await _webFixture.HttpClient.PutAsJsonAsync($"film/images/delete/{filmId}/{image}", new{});

        res.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task AddPerson()
    {
        var filmId = await CreateFilm();
        var personId = await CreatePerson();

        var res = await _webFixture.HttpClient.PutAsJsonAsync($"film/stuff/{filmId}/{personId}", new{});

        res.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task DeletePerson()
    {
        var filmId = await CreateFilm();
        var personId = await CreatePerson();
        var resOfAdding = await _webFixture.HttpClient.PutAsJsonAsync($"film/stuff/{filmId}/{personId}", new{});

        var res = await _webFixture.HttpClient.PutAsJsonAsync($"film/stuff/delete/{filmId}/{personId}", new{});

        res.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task AddArticle()
    {
        var filmId = await CreateFilm();
        var article = RandomText;

        var res = await _webFixture.HttpClient.PutAsJsonAsync($"film/articles/{filmId}/{article}", new{});

        res.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task DeleteArticle()
    {
        var filmId = await CreateFilm();
        var article = RandomText;
        var resOfAdding = await _webFixture.HttpClient.PutAsJsonAsync($"film/articles/{filmId}/{article}", new{});

        var res = await _webFixture.HttpClient.PutAsJsonAsync($"film/articles/delete/{filmId}/{article}", new{});

        res.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task AddTrailer()
    {
        var filmId = await CreateFilm();
        var trailer = RandomText;

        var res = await _webFixture.HttpClient.PutAsJsonAsync($"film/trailers/{filmId}/{trailer}", new{});

        res.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task DeleteTrailer()
    {
        var filmId = await CreateFilm();
        var trailer = RandomText;
        var resOfAdding = await _webFixture.HttpClient.PutAsJsonAsync($"film/trailers/{filmId}/{trailer}", new{});

        var res = await _webFixture.HttpClient.PutAsJsonAsync($"film/trailers/delete/{filmId}/{trailer}", new{});

        res.EnsureSuccessStatusCode();
    }


    [Fact]
    public async Task AddTizer()
    {
        var filmId = await CreateFilm();
        var tizer = RandomText;

        var res = await _webFixture.HttpClient.PutAsJsonAsync($"film/tizers/{filmId}/{tizer}", new{});

        res.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task DeleteTizer()
    {
        var filmId = await CreateFilm();
        var tizer = RandomText;
        var resOfAdding = await _webFixture.HttpClient.PutAsJsonAsync($"film/tizers/{filmId}/{tizer}", new{});

        var res = await _webFixture.HttpClient.PutAsJsonAsync($"film/tizers/delete/{filmId}/{tizer}", new{});

        res.EnsureSuccessStatusCode();
    }


    [Fact]
    public async Task AddRelatedFilm()
    {
        var filmId = await CreateFilm();
        var relateFilmId = await CreateFilm();

        var res = await _webFixture.HttpClient.PutAsJsonAsync($"film/relatefilms/{filmId}/{relateFilmId}", new{});

        res.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task DeleteRelatedFilm()
    {
        var filmId = await CreateFilm();
        var relateFilmId = await CreateFilm();
        var resOfAdding = await _webFixture.HttpClient.PutAsJsonAsync($"film/relatefilms/{filmId}/{relateFilmId}", new{});

        var res = await _webFixture.HttpClient.PutAsJsonAsync($"film/relatefilms/delete/{filmId}/{relateFilmId}", new{});

        res.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task AddNomination()
    {
        var filmId = await CreateFilm();
        var nomination = RandomText;

        var res = await _webFixture.HttpClient.PutAsJsonAsync($"film/nominations/{filmId}/{nomination}", new{});

        res.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task DeleteNomination()
    {
        var filmId = await CreateFilm();
        var nomination = RandomText;
        var resOfAdding = await _webFixture.HttpClient.PutAsJsonAsync($"film/nominations/{filmId}/{nomination}", new{});

        var res = await _webFixture.HttpClient.PutAsJsonAsync($"film/nominations/delete/{filmId}/{nomination}", new{});

        res.EnsureSuccessStatusCode();
    }


    [Fact]
    public async Task AddGenre()
    {
        var filmId = await CreateFilm();
        var genre = RandomText;

        var res = await _webFixture.HttpClient.PutAsJsonAsync($"film/genres/{filmId}/{genre}", new{});

        res.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task DeleteGenre()
    {
        var filmId = await CreateFilm();
        var genre = RandomText;
        var resOfAdding = await _webFixture.HttpClient.PutAsJsonAsync($"film/genres/{filmId}/{genre}", new{});

        var res = await _webFixture.HttpClient.PutAsJsonAsync($"film/genres/delete/{filmId}/{genre}", new{});

        res.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task UpdateSeasons()
    {
        var filmId = await CreateFilm();
        var seasons = new []{
            new {
                Num = 1,
                Serias = new [] {
                    new {
                        Num = 1,
                        IdFile = RandomText
                    },
                    new {
                        Num = 2,
                        IdFile = RandomText
                    }
                },
                Banner = RandomText
            }
        };
        
        var res = await _webFixture.HttpClient.PutAsJsonAsync($"film/seasons/{filmId}", seasons);
        
        res.EnsureSuccessStatusCode();
    }

    async Task<string> CreateFilm()
    {
        var film = new 
        {
            AgeLimit = 18,
            Banner = RandomText,
            Name = RandomText,
            Description = RandomText,
            Country = RandomText,
            KindOfFilm = 0,
            ReleaseType = 0,
            Duration = "00:01:01",
            Release = "2023-02-25",
            StartScreening = "2023-02-25",
            EndScreening = "2023-02-25",
            Content = RandomText,
            Fees = 10,
            Images = new [] {"image"},
            Articles = new [] {"article"},
            Trailers = new [] {"trailer"},
            Tizers = new [] {"tizer"},
            Genres = new [] {RandomText},
            Nominations = new [] {RandomText}, 
        };
        var res =  await _webFixture.HttpClient.PostAsJsonAsync("film", film);
        var jsonFilm = await res.Content.ReadAsStringAsync();
        var jobj = JObject.Parse(jsonFilm);
        var id = jobj["id"].ToString();
        return id;
    }

    async Task<string> CreatePerson()
    {
        var person = new {
            KindOfPerson = 0,
            Birthday = "2023-03-02",
            Name = RandomText,
            Photo = RandomText,
            Height = 10,
            Career = RandomText,
            BirthPlace = RandomText
        };
        var res = await _webFixture.HttpClient.PostAsJsonAsync("person", person);
    
        var json = await res.Content.ReadAsStringAsync();

        var id = JObject.Parse(json)["id"].ToString();

        return id;
    }
}