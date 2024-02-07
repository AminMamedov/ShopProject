namespace Shop.Business.Utilities.Exceptions;

public class AlreadyActiveException:Exception
{
    public AlreadyActiveException(string message) : base(message)
    {

    }
}
