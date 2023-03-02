using Xunit;
using System.Threading.Tasks;
using System.IO;
using System.Net.Http.Json;
using Newtonsoft.Json.Linq;

namespace IntegrationTests;

[Collection("WebContext")]
public class WebCensorIntegrationTest
{
    readonly WebFixture _webFixture;
    string RandomText => Path.GetRandomFileName();
    public WebCensorIntegrationTest (WebFixture webFixture)
    {
        _webFixture = webFixture;
    }

    [Fact]
    public async Task Create()
    {
        var name = RandomText;

        var res = await _webFixture.HttpClient.PostAsJsonAsync($"censor/{name}", new string[0]);

        res.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task Delete()
    {
        var censorId = await CreateCensor();

        var res = await _webFixture.HttpClient.DeleteAsync($"censor/{censorId}");

        res.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task GetOne()
    {
        var censorId = await CreateCensor();

        var res = await _webFixture.HttpClient.GetAsync($"censor/{censorId}");

        res.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task GetMany()
    {
        var censorId = await CreateCensor();

        var res = await _webFixture.HttpClient.GetAsync($"censors");

        res.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task SetFilmsTop()
    {
        var censorId = await CreateCensor();
        var filmId = await CreateFilm();

        var res = await _webFixture.HttpClient.PutAsJsonAsync($"censor/films/{censorId}", new [] { filmId });

        res.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task DeleteFilm()
    {
        var censorId = await CreateCensor();
        var filmId = await CreateFilm();
        var resOfAddingList = await _webFixture.HttpClient.PutAsJsonAsync($"censor/films/{censorId}", new [] { filmId });

        var res = await _webFixture.HttpClient.PutAsJsonAsync($"censor/films/delete/{censorId}/{filmId}", new{});

        res.EnsureSuccessStatusCode();
    }
    
    [Fact]
    public async Task ChangeName()
    {
        var censorId = await CreateCensor();
        var name = RandomText;
        
        var res = await _webFixture.HttpClient.PutAsJsonAsync($"censor/name/{censorId}/{name}", new{});
        

        res.EnsureSuccessStatusCode();
    }

    async Task<string> CreateFilm()
    {
        var film = new 
        {
            AgeLimit = 18,
            Banner = "SOME BANNER",
            Name = "NAME",
            Description = "SOME DESC",
            Country = "COUNTRY",
            KindOfFilm = 0,
            ReleaseType = 0,
            Duration = "00:01:01",
            Release = "2023-02-25",
            StartScreening = "2023-02-25",
            EndScreening = "2023-02-25",
            Content = "CONTENT",
            Fees = 10,
            Images = new [] {"image"},
            Articles = new [] {"article"},
            Trailers = new [] {"trailer"},
            Tizers = new [] {"tizer"},
            Genres = new [] {"полное погружение"},
            Nominations = new [] {"оскар века"}, 
        };
        var res =  await _webFixture.HttpClient.PostAsJsonAsync("film", film);
        var jsonFilm = await res.Content.ReadAsStringAsync();
        var jobj = JObject.Parse(jsonFilm);
        var id = jobj["id"].ToString();
        return id;
    }

    public async Task<string> CreateCensor()
    {
        var name = RandomText;

        var res = await _webFixture.HttpClient.PostAsJsonAsync($"censor/{name}", new string[0]);

        var json = await res.Content.ReadAsStringAsync();

        var id = JObject.Parse(json)["id"].ToString();

        return id;
    }
}
