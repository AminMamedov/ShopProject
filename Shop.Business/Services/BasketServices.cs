using Shop.Business.Interfaces;
using Shop.Core.Entities;
using Shop.DataAccess.DataAccess;
using Shop.Business.Utilities.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Shop.Business.Services;

public class BasketServices : IBasketServices
{
    private ShopDbContext context { get; }
    public BasketServices()
    {
        context = new ShopDbContext();
    }

    public void CrateBasket(int userId)
    {
        var us = context.Users.Find(userId);
        if (us != null)
        {

            Basket basket = new()
            {
                UserId = userId

            };
            context.Baskets.Add(basket);
            context.SaveChanges();



        }
    }

   
    public void ShowBasketProducts(int userId)
    {
        var us = context.Users.Find(userId);

        if (us != null)
        {
            var bas = context.Baskets.FirstOrDefault(b => b.UserId == userId);
            if (bas is null) throw new NotFoundException($"User with name {us.Username} doesn't have a basket");
            if (bas.ProductCount == 0) throw new DoesNotExistException("There is not any product in your basket");
            if (us.URegistr == true)
            {
                var basketProducts = context.BasketProducts.ToList();

                foreach (var product in basketProducts)
                {
                    if (product is not null && product.BasketID == bas.Id)
                    {
                        var pro = context.Products.FirstOrDefault(p => p.Id == product.ProductId);
                        if (pro != null)
                        {
                            Console.WriteLine($"Product name: {pro.Name}; product price: {pro.Price}");
                        }
                    }
                }
            }
        }
    }

}
