using System;
using Microsoft.AspNetCore.Mvc.Testing;
using static GrpcFilmService.FilmServiceProto;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using Grpc.Net.Client;
using MongoDB.Driver;
using Core.Repositories;
using Microsoft.AspNetCore.Hosting;

namespace IntegrationTests;

public class WebFixture : WebApplicationFactory<Program>, IDisposable
{
    private bool disposed = false;
    readonly string DB_NAME = "kip_content_test_db";
    readonly string CLIENT_CONNECTION_TO_GRPC_CHANEL = "http://localhost:5001";

    public IMongoClient MongoClient { get; }

    public HttpClient HttpClient { get; }

    public FilmServiceProtoClient GrpcClient { get; }

    

    public FilmRepository FilmRepository { get; }
    public WebFixture()
    {
        SetEnvironmentVariable();
        MongoClient = Services.GetRequiredService<IMongoClient>();
        GrpcClient = Services.GetRequiredService<FilmServiceProtoClient>();
        HttpClient = CreateClient();
        FilmRepository = Services.GetRequiredService<FilmRepository>();
    }
    private void SetEnvironmentVariable()
    {
        Environment.SetEnvironmentVariable("DB_NAME", DB_NAME);
    }
    protected override void Dispose(bool disposing)
    {
        if (disposed) return;
        if (disposing)
        {
            MongoClient.DropDatabase(DB_NAME);
        }
        disposed = true;
        base.Dispose();
    }


    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Test").ConfigureServices(services =>
        {
            services
                .AddGrpcClient<FilmServiceProtoClient>(options => options.Address = new Uri(CLIENT_CONNECTION_TO_GRPC_CHANEL))
                .ConfigurePrimaryHttpMessageHandler(() => this.Server.CreateHandler());
        });
    }
}
