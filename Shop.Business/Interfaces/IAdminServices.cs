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
    void ActivateDiscount(int discountId);
    void UpdateProductDiscount(int? discountId, int? productId);
    void ShowAllDiscounts();
    

    #endregion
    #region baskets
    void ShowAllBaskets();

    #endregion
    #region product
    void  CreateProduct(string name, decimal price,int proCount, int brandId, int categoryId);
    void DeleteProduct(int productId);
    void ShowDeactiveProducts();
    void ActivateProduct(int productId);
    void ShowAllProducts();
    void ShowDeactiveDiscounts();
    #endregion
    void AdminUserLogin(string userName, string password);
    void AdminUserLogout();
    void CreateCategory(string name);
    void ShowAllCategory();
    void ShowAllBrands();
    void ShowAllInvoices();
    void CreateBrand(string name);

}
