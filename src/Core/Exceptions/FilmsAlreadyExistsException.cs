namespace Core;

public class FilmsAlreadyExistsException : Exception
{
    public FilmsAlreadyExistsException(object film, object place) : base($"Film({film}) arlready exists in {place}"){

    }
}
