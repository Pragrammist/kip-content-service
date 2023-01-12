using Mapster;
using Core;
using Core.Dtos;
using Core.Repositories;
using MongoDB.Driver;
using Core.Dtos.CreatePersonDtos;
using Core.Dtos.UpdatePersonDtos;

namespace Infrastructure.Repositories;

public class PersonRepositoryImpl : PersonRepository
{
    FilterDefinition<Person> FilterById(string id) => Builders<Person>.Filter.Eq(cens => cens.Id, id);

    readonly IMongoCollection<Person> _censorMongoRepo;

    public PersonRepositoryImpl (IMongoCollection<Person> censorMongoRepo) 
    {
        _censorMongoRepo = censorMongoRepo;
    }
    public async Task AddFilm(string id, string filmId, CancellationToken token = default) =>
        await _censorMongoRepo.UpdateOneAsync(
            filter: FilterById(id),
            update: Builders<Person>.Update.AddToSet(per => per.Films, filmId),
            cancellationToken: token
        );
    
    public async Task AddNomination(string id, string nomination, CancellationToken token = default) =>
        await _censorMongoRepo.UpdateOneAsync(
            filter: FilterById(id),
            update: Builders<Person>.Update.AddToSet(per => per.Nominations, nomination),
            cancellationToken: token
        );

    public async Task<PersonDto> Create(CreatePersonDto person, CancellationToken token = default)
    {
        var personToCreate = person.Adapt<Person>();
        await _censorMongoRepo.InsertOneAsync(personToCreate, options: null, token);
        return personToCreate.Adapt<PersonDto>();
    }

    public async Task Delete(string id, CancellationToken token = default) =>
        await _censorMongoRepo.DeleteOneAsync(
            filter: FilterById(id),
            cancellationToken: token
        );
    

    public async Task DeleteFilm(string id, string filmId, CancellationToken token = default) =>
        await _censorMongoRepo.UpdateOneAsync(
            filter: FilterById(id),
            update: Builders<Person>.Update.Pull(per => per.Films, filmId)
        );

    public async Task DeleteNomination(string id, string nomination, CancellationToken token = default) =>
        await _censorMongoRepo.UpdateOneAsync(
            filter: FilterById(id),
            update: Builders<Person>.Update.Pull(per => per.Nominations, nomination)
        );

    public async Task<PersonDto?> Get(string id, CancellationToken token = default) =>
        (await _censorMongoRepo.FindAsync(
            filter:FilterById(id),
            cancellationToken: token
        ))
        .FirstOrDefault() 
        .Adapt<PersonDto>();

    public async Task<IEnumerable<PersonDto>> Get(uint limit = 20, uint page = 1, CancellationToken token = default)=>
        (await _censorMongoRepo.FindAsync(
            filter:Builders<Person>.Filter.Empty,
            options: new FindOptions<Person>
            {
                Limit = (int)limit,
                Skip = (int)(limit * (page - 1))
            },
            cancellationToken: token
        ))
        .ToEnumerable()
        .Adapt<IEnumerable<PersonDto>>();

    public async Task<PersonDto> UpdateData(string id, UpdatePersonDto dataToUpdate, CancellationToken token = default) =>
        (await _censorMongoRepo.FindOneAndUpdateAsync(
            filter: FilterById(id),
            update: BuildPersonUpdate(dataToUpdate),
            cancellationToken: token
        ))
        .Adapt<PersonDto>();
    
    UpdateDefinition<Person> BuildPersonUpdate(UpdatePersonDto dataToUpdate)
    {
        var updList = new List<UpdateDefinition<Person>>();

        if(dataToUpdate.Birthday is not null)
            updList.Add(Builders<Person>.Update.Set(per => per.Birthday, dataToUpdate.Birthday));
        
        if(dataToUpdate.KindOfPerson is not null)
            updList.Add(Builders<Person>.Update.Set(per => per.KindOfPerson, (PersonType)dataToUpdate.KindOfPerson));

        if(dataToUpdate.Name is not null)
            updList.Add(Builders<Person>.Update.Set(per => per.Name, dataToUpdate.Name));

        if(dataToUpdate.Height is not null)
            updList.Add(Builders<Person>.Update.Set(per => per.Height, dataToUpdate.Height));

        if(dataToUpdate.Photo is not null)
            updList.Add(Builders<Person>.Update.Set(per => per.Photo, dataToUpdate.Photo));
            
        if(dataToUpdate.Career is not null)
            updList.Add(Builders<Person>.Update.Set(per => per.Career, dataToUpdate.Career));

        if(dataToUpdate.BirthPlace is not null)
            updList.Add(Builders<Person>.Update.Set(per => per.BirthPlace, dataToUpdate.BirthPlace));

        UpdateDefinition<Person> finalUpdate = Builders<Person>.Update.Combine(updList);

        return finalUpdate;
    }
}