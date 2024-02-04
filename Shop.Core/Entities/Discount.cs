using Shop.Core.Interface;

namespace Shop.Core.Entities;

public  class Discount:BaseEntities
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;
    public decimal Percentage { get; set; }
    public bool IsActive { get; set; }  
    public ICollection<Product> Product { get; set; }
    

}
