namespace Shop.Core.Interface;

public abstract class BaseEntities
{
    public DateTime CreateTime { get; set; } = DateTime.Now;
    public DateTime UpdateTime { get; set; } = DateTime.Now;
}
