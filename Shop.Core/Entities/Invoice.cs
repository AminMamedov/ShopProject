namespace Shop.Core.Entities;

public class Invoice
{
    public int Id { get; set; }
    public decimal TotalPrice { get; set; }
    public int UserId { get; set; } 
    public ICollection<ProductInvoice> ProductInvoice { get; set; }
    public User User { get; set; }

}
