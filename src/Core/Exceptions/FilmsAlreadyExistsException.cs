namespace Core;

public class AppException : Exception
{
    public AppException(){}
    public AppException(string message) {}
}


public class FilmNotValidException : AppException
{
    public FilmNotValidException(object film, string? message = null) : base($"Film({film}) arlready exists in { message ?? "film is not valid" }"){

    }
}
