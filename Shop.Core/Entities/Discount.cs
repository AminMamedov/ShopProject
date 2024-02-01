using Shop.Core.Interface;

namespace Shop.Core.Entities;

public  class Discount:BaseEntities
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Percentage { get; set; }

}
