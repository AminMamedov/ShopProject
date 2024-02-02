namespace Shop.Business.Interfaces;

public interface IBasketServices
{
    void CratedBasket(int userId);
    void ShowBasketProducts(int userId);
}
