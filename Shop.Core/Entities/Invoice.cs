namespace Shop.Core.Entities;

public class Invoice
{
    public int Id { get; set; }
    public decimal TotalPrice { get; set; }
    ICollection<ProductInvoice> ProductInvoice { get; set; }

}
