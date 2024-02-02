using System.Collections.Specialized;

namespace Shop.Business.Interfaces;

public interface IUserServices
{
    void CreateUser(string userName, string email, string password);

    void UpdateUser(int userId , string password,string newUname, string newPassword,  string newEmail);
    void DeleteUser(int userId, string password);    
    void GetUserWallets(int userId);
    void LogIn(string userName ,string email, string password);
    void LogOut();


}
