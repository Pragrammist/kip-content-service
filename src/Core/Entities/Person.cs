namespace Core;

public class Person //знаменитость
{

    private Person(){}

    public Person(string name, DateTime birthday, string photo, 
    string career, string birthPlace, uint height, 
    string? id = null, PersonType kindOfPerson = default, 
    List<string>? nominations = null, List<string>? films = null)
    {
        Films = films ?? new List<string>();

        if(Films.Any(f => string.IsNullOrEmpty(f)))
            throw new NullOrEmptyInnerCollectionException(nameof(films), nameof(Person));

        Nominations = nominations ?? new List<string>();

        if(Nominations.Any(n => string.IsNullOrEmpty(n)))
            throw new NullOrEmptyInnerCollectionException(nameof(nominations), nameof(Person));

        KindOfPerson = kindOfPerson; 
        KindOfPerson = kindOfPerson;

        Name = name;

        if(string.IsNullOrEmpty(Name))
            throw new FieldIsNullOrEmptyException(nameof(Name), nameof(Person));

        Photo = photo;

        if(string.IsNullOrEmpty(Photo))
            throw new FieldIsNullOrEmptyException(nameof(Photo), nameof(Person));

        Career = career;

        if(string.IsNullOrEmpty(Career))
            throw new FieldIsNullOrEmptyException(nameof(Career), nameof(Person));

        Height = height;
        Birthday = birthday;

        BirthPlace = birthPlace;   

        if(string.IsNullOrEmpty(BirthPlace))
            throw new FieldIsNullOrEmptyException(nameof(BirthPlace), nameof(Person));
    }
    public string Id { get; set; }  = null!;

    public PersonType KindOfPerson { get; set; }

    public DateTime Birthday { get; set; }

    public string Name { get; set; }  = null!; 

    public string Photo { get; set; } = null!;

    public uint Height { get; set; }

    public string Career { get; set; } = null!; 

    public string BirthPlace { get; set; } = null!;

    public List<string> Films { get; set; } = new List<string>();

    public List<string> Nominations { get; set; } = new List<string>();
}
public class NullOrEmptyInnerCollectionException : Exception
{
    public NullOrEmptyInnerCollectionException(string collName, string objName) : base($"collection {collName} inner {objName} has null inner collection")
    {

    }
}

public class FieldIsNullOrEmptyException : Exception
{
    public FieldIsNullOrEmptyException(string fieldName, string objName) : base($"field {fieldName} inner {objName} has null")
    {

    }
}
