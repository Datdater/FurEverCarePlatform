namespace FurEverCarePlatform.Application.Exception;

public class NotFoundException : System.Exception
{
    public NotFoundException(string name, object key) : base($"{name} with ID {key} was not found")
    {

    }
}