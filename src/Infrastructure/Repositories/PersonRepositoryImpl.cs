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

    readonly IMongoCollection<Person> _personMongoRepo;
    readonly IMongoCollection<Film> _filmMongoRepo;
    public PersonRepositoryImpl (IMongoCollection<Person> personCol, IMongoCollection<Film> filmCol) 
    {
        
        _personMongoRepo = personCol;

        _filmMongoRepo = filmCol;
    }
    
    public async Task<bool> AddNomination(string id, string nomination, CancellationToken token = default) =>
        (await _personMongoRepo.UpdateOneAsync(
            filter: FilterById(id),
            update: Builders<Person>.Update.AddToSet(per => per.Nominations, nomination),
            cancellationToken: token
        ))
        .ModifiedCount > 0;

    public async Task<PersonDto> Create(CreatePersonDto person, CancellationToken token = default)
    {
        var personToCreate = person.Adapt<Person>();
        await _personMongoRepo.InsertOneAsync(personToCreate, options: null, token);
        return personToCreate.Adapt<PersonDto>();
    }

    public async Task<bool> Delete(string id, CancellationToken token = default) 
    {
        var isDeleted = (await _personMongoRepo.DeleteOneAsync(
            filter: FilterById(id),
            cancellationToken: token
        )).DeletedCount > 0;
        if(!isDeleted)
            return false;
        
        var deleteFilmIdFromPersonRes = await _filmMongoRepo.UpdateManyAsync(
            filter: Builders<Film>.Filter.AnyEq(f => f.Stuff, id),
            update: Builders<Film>.Update.Pull(f => f.Stuff, id),
            cancellationToken: token  
        );
        var idIsDeleted =  deleteFilmIdFromPersonRes.ModifiedCount > 0 && deleteFilmIdFromPersonRes.MatchedCount > 0 || deleteFilmIdFromPersonRes.MatchedCount == 0;


        return idIsDeleted && isDeleted;
    }
        
    
    

    public async Task<bool> DeleteNomination(string id, string nomination, CancellationToken token = default) =>
        (await _personMongoRepo.UpdateOneAsync(
            filter: FilterById(id),
            update: Builders<Person>.Update.Pull(per => per.Nominations, nomination)
        ))
        .ModifiedCount > 0;

    public async Task<PersonDto?> Get(string id, CancellationToken token = default) =>
        (await _personMongoRepo.FindAsync(
            filter:FilterById(id),
            cancellationToken: token
        ))
        .FirstOrDefault() 
        .Adapt<PersonDto>();

    public async Task<IEnumerable<PersonDto>> Get(int limit, int skip, CancellationToken token = default)=>
        (await _personMongoRepo.FindAsync(
            filter:Builders<Person>.Filter.Empty,
            options: new FindOptions<Person>
            {
                Limit = limit,
                Skip = skip
            },
            cancellationToken: token
        ))
        .ToEnumerable()
        .Adapt<IEnumerable<PersonDto>>();

    public async Task<bool> UpdateData(string id, UpdatePersonDto dataToUpdate, CancellationToken token = default) =>
        (await _personMongoRepo.UpdateOneAsync(
            filter: FilterById(id),
            update: BuildPersonUpdate(dataToUpdate),
            cancellationToken: token
        ))
        .ModifiedCount > 0;
    
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
        
        if(dataToUpdate.Nominations is not null)
            updList.Add(Builders<Person>.Update.Set(p => p.Nominations, dataToUpdate.Nominations));

        UpdateDefinition<Person> finalUpdate = Builders<Person>.Update.Combine(updList);

        return finalUpdate;
    }
}