namespace Shop.Business.Interfaces;

public interface IBasketServices
{
    void CrateBasket(int userId);
    void ShowBasketProducts(int userId);
    void BuyProduct(int userId, int productId, int walletId, int count);
}
