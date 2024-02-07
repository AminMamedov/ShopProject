namespace Shop.Core.Entities;

public class ShowInvoicesVW
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string username { get; set; }
    public string DeliveryAddress { get; set; }
    public string ProductName { get; set; }
    public int ProductCount { get; set; }
    public decimal TotalPrice { get; set; }
    public DateTime CreateTime { get; set; }
}
