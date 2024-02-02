using Shop.Business.Interfaces;
using Shop.Core.Entities;
using Shop.DataAccess.DataAccess;
using Shop.Business.Utilities.Exceptions;

namespace Shop.Business.Services;

public class BasketServices : IBasketServices
{
    ShopDbContext context = new ShopDbContext();

    public void CratedBasket(int userId)
    {
        var us = context.Users.Find(userId);
        if (us != null)
        {
            if (us.SignIn == true)
            {
                Basket basket = new()
                {
                    UserId = us.Id
                    

                };
            }
            
            
        }
    }

    public void ShowBasketProducts(int userId)
    {
        var us = context.Users.Find(userId);
        var bas = context.Baskets.FirstOrDefault(b => b.UserId == userId);
        if (bas is null) throw new NotFoundException($"User with name {us.Username} doesn't have basket");
        if(us != null)
        {
            if (us.SignIn == true)
            {
                foreach(var product in context.BasketProducts)
                {
                    if (product.BaskerID == bas.Id)
                    {
                        Console.WriteLine( $"Product name:{product.Product.Name}; product price :{product.Product.Price}");
                    }
                }
            }
        }
    }
}
