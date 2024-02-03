namespace Shop.Business.Interfaces;

public interface IProductServices
{
    void ShowAllProducts();
    void AddProductToBasket(int productId, int userId, int productCount);
    void DeleteProductFromBasket(int productId, int userId , int productCount );
}
