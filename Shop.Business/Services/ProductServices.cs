using Shop.Business.Interfaces;
using Shop.Core.Entities;
using Shop.DataAccess.DataAccess;
using Shop.Business.Utilities.Exceptions;

namespace Shop.Business.Services;

public class ProductServices : IProductServices
{
    ShopDbContext context = new ShopDbContext();

    public void AddProductToBasket(int productId,int basketId)
    {
        var bas = context.Baskets.Find(basketId);
        if (bas != null)
        {
            var us = context.Users.Find(bas.UserId);
            if (us != null)
            {
                if(us.SignIn == true)
                {
                    var pro = context.Products.Find(productId);
                    if (pro is null) throw new NotFoundException($"Product with id :{productId} doesn't exist");
                    BasketProduct basketProduct = new()
                    {
                        ProductId = productId,
                        BasketID = basketId,
                        
                    };
                    bas.ProductCount++;
                    context.BasketProducts.Add(basketProduct);
                    context.SaveChanges();
                }
            }


        }
    }

    public void ShowAllProducts()
    {
        foreach(var product in context.Products)
        {
            Console.WriteLine($"Product Id :{product.Id} ; product name:{product.Name} ; product price: {product.Price}");
        }
    }
}
