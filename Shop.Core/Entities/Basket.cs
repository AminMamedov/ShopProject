namespace Shop.Core.Entities;

public class Basket
{
    public int Id { get; set; }
    public int UserId { get; set; } 
    public User User { get; set; }
    public ICollection<BasketProduct> BasketProduct { get; set; }

}
