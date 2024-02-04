namespace Shop.Business.Interfaces;

public  interface IAdminServices
{
    #region user
    void ShowAllUsers();
    #endregion
    #region wallet
    void ShowAllWallets();

    #endregion
    #region discount
    void CreateDiscount(string name, int percentage);
    void DisableDiscount(int discountId);
    void AddDiscountToProduct(int productId, int discountId);
    

    #endregion
    #region baskets
    void ShowAllBaskets();

    #endregion
    #region product
    void  CreateProduct(string name, decimal price, int discountId, int proCount);
    void DeleteProduct(int productId);
    void ShowAllProducts();
    #endregion
    void AdminUserLogin(string userName,string email, string password);
    void AdminUserLogout();
}
