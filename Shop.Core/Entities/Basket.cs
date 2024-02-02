namespace Shop.Core.Entities;

public class Basket
{
    public int Id { get; set; }
    public int UserId { get; set; } 
    public int ProductCount { get; set; } = 0;
    public User User { get; set; }
    public ICollection<BasketProduct> BasketProduct { get; set; }

}
