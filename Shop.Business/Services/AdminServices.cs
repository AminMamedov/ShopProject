using Shop.Business.Interfaces;
using Shop.Business.Utilities.Exceptions;
using Shop.Core.Entities;
using Shop.DataAccess.DataAccess;

namespace Shop.Business.Services;

public class AdminServices : IAdminServices
{
    private ShopDbContext context { get; }
    public AdminServices()
    {
        context = new ShopDbContext();
    }
    public void AddDiscountToProduct(int productId, int discountId)
    {
        var pro = context.Products.Find(productId);
        if (pro is null) throw new DoesNotExistException($"Product with Id :{productId} doesn't exist");
        var dis = context.Discounts.Find(discountId);
        if (dis is null) throw new DoesNotExistException($"Discount with Id :{discountId} doesn't exist");
        if (pro.DiscountId != null) throw new AlreadyExistException($"This product already has a discount");
        if (pro.DiscountId == discountId) throw new AlreadyExistException($"This product already has this discount");
        if (dis.IsActive == false) throw new DoesNotExistException($"Discount with Id :{discountId} disabled");
        pro.DiscountId = discountId;
        pro.Price = pro.Price - (pro.Price * dis.Percentage / 100);
        context.SaveChanges();

    }

    public void CreateDiscount(string name, int percentage)
    {
        var dis = context.Discounts.FirstOrDefault(d => d.Name == name);
        if (dis is not null) throw new AlreadyExistException($"Discount with name {name} already is exist");
        Discount discount = new()
        {
            Name = name,
            Percentage = percentage

        };
        discount.IsActive = true;
        context.Discounts.Add(discount);
        context.SaveChanges();
    }

    public void CreateProduct(string name, decimal price, int discountId, int proCount)
    {

        var pro = context.Products.FirstOrDefault(p => p.Name.ToLower() == name.ToLower());
        if (pro is not null) throw new AlreadyExistException($"Product with name :{name} already is exist");
        Product product = new()
        {
            Name = name,
            Price = price,
            DiscountId = discountId,
            ProductCount = proCount
        };
        context.Products.Add(product);
        context.SaveChanges();



    }

    public void DeleteProduct(int productId)
    {
        var pro = context.Products.Find(productId);
        if (pro is null) throw new DoesNotExistException($"Product with Id :{productId} doesn't exist");
        if (pro.ProductCount > 0) throw new StillHasProCountInStockException($"Product with Id :{productId} still exist in product stock");
        context.Products.Remove(pro);
        context.SaveChanges();
           
        
    }

    public void DisableDiscount(int discountId)
    {
        var dis = context.Discounts.Find(discountId);
        if(dis is null) throw new DoesNotExistException($"Discount with Id :{discountId} doesn't exist");
        if (dis.IsActive == false) throw new AlreadyDoesNotActiveException($"Discount with Id :{discountId} already deactivated");
        dis.IsActive = false;
        foreach (var product in context.Products)
        {
            if(product.DiscountId == discountId)
            {
                product.DiscountId = null;
                product.Price = product.Price + (product.Price * dis.Percentage / 100);

            }
        }
        context.SaveChanges();
    }

    

    public void ShowAllBaskets()
    {
        foreach(var basket in context.Baskets)
        {
            Console.WriteLine($"Basket Id:{basket.Id}; Basket's userId{basket.UserId}; Basket's product count:{basket.ProductCount}");

        }
    }

    public void ShowAllProducts()
    {
        foreach (var product in context.Products)
        {
            var dis = context.Discounts.Find(product.DiscountId);
            if (dis is null)
            {
            Console.WriteLine($"Product Id :{product.Id} ; product name:{product.Name} ; product price: {product.Price}");

            }
            else
            {
                Console.WriteLine($"Product Id :{product.Id} ; product name:{product.Name} ; product price: {product.Price} ; product discountId:{product.DiscountId}");

            }



        }
    }

    public void ShowAllUsers()
    {
        foreach( var user in context.Users)
        {
            Console.WriteLine($"User Id:{user.Id}; user name{user.Username} ; ");
        };
    }

    public void ShowAllWallets()
    {
        foreach(var wallet in context.Wallets)
        {
            Console.WriteLine($"Wallet Id:{wallet.Id} wallet's userId:{wallet.UserId}; CardNumber: {wallet.CardNumber}; cardBalance: {wallet.CardBalance}");
        }
    }
}
