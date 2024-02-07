using Shop.Core.Interface;

namespace Shop.Core.Entities;

public class Product : BaseEntities
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }
    public int? DiscountId { get; set; }
    public int? CategoryId { get; set; }
    public int? BrandId { get; set; }
    public int ProductCount { get; set; }
    public bool IsActive { get; set; } = true;

    public ICollection<BasketProduct> BasketProduct { get; set; }
    public ICollection<ProductInvoice> ProductInvoice { get; set; }

    public Brand Brand { get; set; }
    public Category Category { get; set; }
    public Discount Discount { get; set; }
}
