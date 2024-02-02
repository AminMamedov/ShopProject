namespace Shop.Business.Interfaces;

public interface IProductServices
{
    void ShowAllProducts();
    void AddProductToBasket(int productId, int basketId);
}
