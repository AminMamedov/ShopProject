using Shop.Business.Interfaces;
using Shop.Business.Utilities.Exceptions;
using Shop.Core.Entities;
using Shop.DataAccess.DataAccess;
using System.Collections.Generic;

namespace Shop.Business.Services;

public class UserServices:IUserServices
{
    ShopDbContext context = new ShopDbContext();
    public void CreateUser(string userName,string email, string password )
    {
        if (string.IsNullOrEmpty(userName)) throw new ArgumentNullException();
        if (string.IsNullOrEmpty(email)) throw new ArgumentNullException();
        if (string.IsNullOrEmpty(password)) throw new ArgumentNullException();

        var result = context.Users.FirstOrDefault(u => u.Username == userName);
        if (result is not null) throw new AlreadyExistException($"This username is already exist");
        var result1 = context.Users.FirstOrDefault(u => u.Email == email);
        if (result1 is not null) throw new AlreadyExistException($"User with this email is already exist");

        User user = new()
        {
            Username = userName,
            Email = email,
            Password = password
        };
        context.Users.Add(user);
        context.SaveChanges();

    }

    public void DeleteUser(int userId, string password)
    {
        var u1 = context.Users.Find(userId);
        if(u1 is null) throw new NotFoundException($"User with id:{userId} was not found.");
        if (u1.Password != password) throw new IncorrectExeption("Incorrect password,try again");
        context.Users.Remove(u1);
        context.SaveChanges();
    }

    public void UpdateUser(int userId,string password, string newUname, string newPassword, string newEmail)
    {
        var u1 = context.Users.Find(userId);
        if (u1 is null) throw new NotFoundException($"User with id:{userId} was not found.");
        if (u1.Password != password) throw new IncorrectExeption("Incorrect password,try again");
        context.Users.Remove(u1);
        User user = new()
        {
            Username = newUname,
            Email = newEmail,
            Password = newPassword
        };
        context.Users.Add(user);
        context.SaveChanges();
    }
    public void GetUserWallets(int userId, string password)
    {
        throw new NotImplementedException();
    }

}
