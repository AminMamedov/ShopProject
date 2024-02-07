using System.Collections.Specialized;

namespace Shop.Business.Interfaces;

public interface IUserServices
{
    void CreateUser(string firstName, string lastName, string phone,string deliveryadd, string userName, string email, string password);

    void UpdateUser(int? userId , string password, string newUname, string newPassword,  string newEmail);
    void UpdateUserDetails(int? userId, string newPhone, string newDeliveryadd);
    void DeleteUser(int? userId, string password);    
    void GetUserWallets(int? userId);
    void LogIn(string userName ,string email, string password);
    void LogOut();
     //"-------------REGISTER--------------\n" +
     //                                    " 1 - Delete Account\n" +
     //                                    " 2 - Update acc login info\n" +
     //                                    " 3 - Update your informations\n" +
     //                                    " 4 - Show wallets\n" +
     //                                    " 5 - Add new wallet\n" +
     //                                    " 6 - Delete wallet\n" +
     //                                    " 7 - Show products in basket\n" +
     //                                    " 8 - Buy product\n" +
     //                                    " 9 - Show all products\n" +
     //                                    " 10 - Add product to basket\n" +
     //                                    " 11 - Delete product from basket\n" +
     //                                    " 12 - Show invoices\n" +
     //                                    " 13 - LogOut\n" + Delete Account
     //                                    "----------------------------------");

}
