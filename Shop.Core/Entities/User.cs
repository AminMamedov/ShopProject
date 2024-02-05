using Shop.Core.Interface;

namespace Shop.Core.Entities;

public class User : BaseEntities
{
    //tpc
    public int Id { get; set; }
    public string FirsName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Phone {  get; set; } = null!;
    public string DeliveryAddress { get; set; } = null!;    
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Email { get; set; } = null!;
    public bool URegistr { get; set; } = false;
    public bool ARegistr { get; set; } = false;
    public bool IsAdminUser { get; set; } = false;
    public bool IsDeleted {  get; set; } = false;
    public Basket Basket { get; set; }
    public ICollection<Wallet> Wallet { get; set; }
    public ICollection<Invoice> Invoice { get; set; }
}
