namespace Shop.Business.Utilities.Helpers;

public enum Register
{
    Login=1,
    LoginAsAdmin,
    CreateAccount
}
public enum UserMethods
{
    DeleteUser=1,
    UpdateUser,
    UpdateUserDetails,
    GetUserWallets,
    AddWallet,
    DeleteWallet,
    ShowBasketProducts,
    BuyProduct,
    ShowAllProducts,
    AddProductToBasket,
    DeleteProductFromBasket,
    ShowUserInvoices,
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
    AdminUserLogout
}
