using System.Security.Cryptography.X509Certificates;
using Core.Repositories;
using Core.Interactors;
using Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Configuration;

public static class AppServicesExtension
{
    public static IServiceCollection AddCensorRepository(this IServiceCollection services)
        => services.AddTransient<CensorRepository, CensorRepositoryImpl>();
    
    public static IServiceCollection AddPersonRepository(this IServiceCollection services)
        => services.AddTransient<PersonRepository, PersonRepositoryImpl>();

    public static IServiceCollection AddFilmRepositories(this IServiceCollection services)
        => services.AddTransient<FilmRepository, FilmRepositoryImpl>()
                    .AddTransient<EntityFilmRepository, EntityFilmRepositoryImpl>();
    
    public static IServiceCollection AddFilmSelectionRepository(this IServiceCollection services)
        => services.AddTransient<FilmSelectionRepository, FilmSelectionRepositoryImpl>();

    public static IServiceCollection AddFilmInteractor(this IServiceCollection services)
        => services.AddTransient<FilmInteractor>();

    public static IServiceCollection AddAppServices(this IServiceCollection services)
    {
        MapsterConfiguration.ConfigureMapsterGlobally();
        services.AddCensorRepository()
                   .AddPersonRepository()
                   .AddFilmRepositories()
                   .AddFilmSelectionRepository()
                   .AddFilmInteractor();
        return services;
    }
        
}