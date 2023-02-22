using System.Reflection;
using Web.GrpcServices;
using Serilog;
using Serilog.Events;
using Prometheus;
using Infrastructure.Configuration;

public class Program
{
    public static void Main(string[] args)
    {
        try
        {
            MongoDbConfiguration.ConfigureMongoDbGlobally();
            MapsterConfiguration.ConfigureMapsterGlobally();

            var logstashUrl = Environment.GetEnvironmentVariable("LOGSTASH_URL") ?? "http://localhost:8080";
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
                .WriteTo.Console()
                .WriteTo.Http(logstashUrl, queueLimitBytes: null)
                .CreateLogger();


            

            var builder = WebApplication.CreateBuilder(args);

            builder.Host.UseSerilog();

            var configuration = builder.Configuration;


            var mongoConnection = configuration["MONGODB_CONNECTION_STRING"] ?? "mongodb://localhost:27017";
            var dbName = configuration["DB_NAME"] ?? "kip_content_service_db";
            var censorsCollection = configuration["CENSOR_COLLECTION_NAME"] ?? "censors";
            var personsCollection = configuration["PERSON_COLLECTION_NAME"] ?? "persons";
            var filmsCollection = configuration["FILM_COLLECTION_NAME"] ?? "films";
            var filmSelectionsCollection = configuration["FILM_COLLECTION_NAME"] ?? "selections";

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddGrpc();

            builder.Services.AddMongoDb(mongoConnection, dbName)
                            .AddCensorMongoDbCollection(censorsCollection)
                            .AddPersonMongoDbCollection(personsCollection)
                            .AddFilmMongoDbCollection(filmsCollection)
                            .AddFilmSelectionMongoDbCollection(filmSelectionsCollection);
            
            builder.Services.AddCensorRepository()
                            .AddPersonRepository()
                            .AddFilmRepositories()
                            .AddFilmInteractor();
            
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer()
                            .AddSwaggerGen(options => {
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename), includeControllerXmlComments: true);
            });
            

            var app = builder.Build();

            app.UseSerilogRequestLogging();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                
            }


            app.UseMetricServer();
            app.UseHttpMetrics(options => options.ReduceStatusCodeCardinality());
            app.UseGrpcMetrics();

            app.UseSwagger();
            app.UseSwaggerUI(options => {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                options.RoutePrefix = string.Empty;
            });

            app.UseAuthorization();

            app.MapMetrics();
            app.MapControllers();
            app.MapGrpcService<FilmServiceGrpc>();

            

            app.Run();
        }
        catch(Exception ex)
        {
            Log.Fatal(ex, "Application terminated unexpectedly");
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }
}

