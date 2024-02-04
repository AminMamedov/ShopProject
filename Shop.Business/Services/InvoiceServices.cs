using Shop.Business.Interfaces;
using Shop.DataAccess.DataAccess;
using Shop.Business.Utilities.Exceptions;


namespace Shop.Business.Services;

public class InvoiceServices : IInvoiceServices
{
    private ShopDbContext context { get; }
    public InvoiceServices()
    {
        context = new ShopDbContext();
    }
    public void ShowUserInvoices(int userId)
    {
        var us = context.Users.Find(userId);
        if (us is null) throw new DoesNotExistException($"User with Id :{userId} doesn't exist");
        foreach(var invoice in context.Invoices)
        {
            if (invoice == null) throw new DoesNotExistException("You doesn't hav any invoices yet");
            if (invoice.UserId == userId)
            {
                Console.WriteLine($"Product name :{invoice.ProductName} ; product count: {invoice.ProductCount}; total price: {invoice.TotalPrice}; created time:{invoice.CreateTime}");
            }
        }
    }
}
