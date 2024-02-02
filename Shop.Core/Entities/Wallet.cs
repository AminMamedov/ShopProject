namespace Shop.Core.Entities;

public class Wallet
{
    public int Id { get; set; }
    public string CardNumber { get; set; }
    public int UserId { get; set; }
    public decimal CardBalance {get; set;}
    public User User { get; set; }
}
