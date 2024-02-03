namespace Shop.Business.Utilities.Exceptions;

public class MoreThanStockCountException : Exception
{
    public MoreThanStockCountException(string message) : base(message)
    {

    }
}