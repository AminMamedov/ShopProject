using Shop.Core.Interface;

namespace Shop.Core.Entities;

public class User : BaseEntities
{
    //tpc
    public int Id { get; set; }
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Email { get; set; } = null!;
    public bool SignIn { get; set; } = false;
    public Basket Basket { get; set; }
    public ICollection<Wallet> Wallet { get; set; }
    public ICollection<Invoice> Invoice { get; set; }
}
