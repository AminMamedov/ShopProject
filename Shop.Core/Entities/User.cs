using Shop.Core.Interface;

namespace Shop.Core.Entities;

public class User : BaseEntities
{
    //tpc
    public int Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
}
