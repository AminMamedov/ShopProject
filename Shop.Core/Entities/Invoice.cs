using Shop.Core.Interface;

namespace Shop.Core.Entities;

public class Invoice : BaseEntities
{
    public int Id { get; set; }
    public string ProductName { get; set; } = null!;
    public int ProductCount { get; set; }
    public decimal TotalPrice { get; set; }
    public int UserId { get; set; } 
    public bool Status { get; set; }= false;
    public ICollection<ProductInvoice> ProductInvoice { get; set; }
    public User User { get; set; }

}
