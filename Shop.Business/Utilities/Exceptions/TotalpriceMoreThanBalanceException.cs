namespace Shop.Business.Utilities.Exceptions;

public class TotalpriceMoreThanBalanceException :Exception
{
    public TotalpriceMoreThanBalanceException(string message) : base(message)
    {

    }
}
