namespace Shop.Business.Interfaces;

public interface IBasketServices
{
    void CrateBasket(int userId);
    void ShowBasketProducts(int userId);
}
