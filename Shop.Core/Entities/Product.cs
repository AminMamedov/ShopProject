using Shop.Core.Interface;

namespace Shop.Core.Entities;

public class Product : BaseEntities
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int DiscountId { get; set; }
    ICollection<BasketProduct> BasketProduct { get; set; }
    ICollection<ProductInvoice> ProductInvoice { get; set; }
}
