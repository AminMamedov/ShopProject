namespace Shop.Business.Utilities.Helpers;

public enum Register
{
    Login=1,
    LoginAsAdmin,
    CreateAccount,
    Quit
}
public enum Settings
{
    UpdateUser=1,
    UpdateUserDetails,
    DeleteWallet,
    DeleteUser,
    Back
}
public enum UserMethods
{
    ShowAllProducts=1,
    ShowBasketProducts,
    AddProductToBasket,
    DeleteProductFromBasket,
    GetUserWallets,
    AddWallet,
    BuyProduct,
    ShowUserInvoices,
    GoToSettings,
    LogOut
    
  
}
public enum AdminMethods
{
    ShowAllUsers=1,
    ShowAllWallets,
    CreateDiscount,
    DisableDiscount,
    AddDiscountToProduct,
    ShowAllBaskets,
    CreateProduct,
    DeleteProduct,
    ShowAllProducts,
    CreateCategory,
    CreateBrand,
    AdminUserLogout
}
