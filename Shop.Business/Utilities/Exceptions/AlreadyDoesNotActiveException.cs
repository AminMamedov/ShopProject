namespace Shop.Business.Utilities.Exceptions;

public class AlreadyDoesNotActiveException : Exception
{
    public AlreadyDoesNotActiveException(string message) : base(message)
    {

    }
}