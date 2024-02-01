namespace Shop.Core.Entities;

public class Basket
{
    public int Id { get; set; }
    ICollection<BasketProduct> BasketProduct { get; set; }

}
