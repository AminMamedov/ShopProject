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
    public void AddDiscountToProduct(int discountId, int productId)
    {
        var us = context.Users.FirstOrDefault(u => u.ARegistr == true);
        if (us is null) throw new NotLoggedInException("Login as Admin user to choose this operation ");
        else
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

    }

    public void CreateDiscount(string name, int percentage)
    {
        var us = context.Users.FirstOrDefault(u => u.ARegistr == true);
        if (us is null) throw new NotLoggedInException("Login as Admin user to choose this operation ");
        else
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
    }

    public void CreateProduct(string name, decimal price, int discountId, int proCount)
    {
        var us = context.Users.FirstOrDefault(u => u.ARegistr == true);
        if (us is null) throw new NotLoggedInException("Login as Admin user to choose this operation ");
        else
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



    }

    public void DeleteProduct(int productId)
    {
        var us = context.Users.FirstOrDefault(u => u.ARegistr == true);
        if (us is null) throw new NotLoggedInException("Login as Admin user to choose this operation ");
        else
        {
            var pro = context.Products.Find(productId);
            if (pro is null) throw new DoesNotExistException($"Product with Id :{productId} doesn't exist");
            if (pro.ProductCount > 0) throw new StillHasProCountInStockException($"Product with Id :{productId} still exist in product stock");
            context.Products.Remove(pro);
            context.SaveChanges();

        }


    }

    public void DisableDiscount(int discountId)
    {
        var us = context.Users.FirstOrDefault(u => u.ARegistr == true);
        if (us is null) throw new NotLoggedInException("Login as Admin user to choose this operation ");
        else
        {
            var dis = context.Discounts.Find(discountId);
            if (dis is null) throw new DoesNotExistException($"Discount with Id :{discountId} doesn't exist");
            if (dis.IsActive == false) throw new AlreadyDoesNotActiveException($"Discount with Id :{discountId} already deactivated");
            dis.IsActive = false;
            foreach (var product in context.Products)
            {
                if (product.DiscountId == discountId)
                {
                    product.DiscountId = null;
                    product.Price = product.Price + (product.Price * dis.Percentage / 100);

                }
            }
            context.SaveChanges();

        }
    }
    public void ShowAllDiscounts()
    {
        var us = context.Users.FirstOrDefault(u => u.ARegistr == true);
        if (us is null) throw new NotLoggedInException("Login as Admin user to choose this operation ");
        foreach(var dis in context.Discounts)
        {
            if (dis.IsActive == true)
            {
                Console.WriteLine($"Discount idL:{dis.Id} ; discount name:{dis.Name} ; discount percentage:{dis.Percentage}");
            }
        }
    }



    public void ShowAllBaskets()
    {
        var us = context.Users.FirstOrDefault(u => u.ARegistr == true);
        if (us is null) throw new NotLoggedInException("Login as Admin user to choose this operation ");
        else
        {
            foreach (var basket in context.Baskets)
            {
                Console.WriteLine($"Basket Id:{basket.Id}; Basket's userId{basket.UserId}; Basket's product count:{basket.ProductCount}");

            }

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
        var us = context.Users.FirstOrDefault(u => u.ARegistr == true);
        if (us is null) throw new NotLoggedInException("Login as Admin user to choose this operation ");
        else
        {
            foreach (var user in context.Users)
            {
                Console.WriteLine($"User Id:{user.Id}; user name{user.Username} ; ");
            };

        }
    }

    public void ShowAllWallets()
    {
        var us = context.Users.FirstOrDefault(u => u.ARegistr == true);
        if (us is null) throw new NotLoggedInException("Login as Admin user to choose this operation ");
        else
        {

            foreach (var wallet in context.Wallets)
            {
                Console.WriteLine($"Wallet Id:{wallet.Id} wallet's userId:{wallet.UserId}; CardNumber: {wallet.CardNumber}; cardBalance: {wallet.CardBalance}");
            }
        }
    }

    public void AdminUserLogin(string userName, string email, string password)
    {
        var u1 = context.Users.FirstOrDefault(u => u.Username == userName);
        if (u1 is null)
        {
            var u2 = context.Users.FirstOrDefault(u1 => u1.Email == email);
            if (u2 is null) throw new DoesNotExistException($"Admin user with name:{userName} and email:{email} doesn't exist");
            if (u2.Password == password && u2.IsAdminUser == true)
            {
                u2.ARegistr = true;
            }
            context.SaveChanges();

        }
        else
        {
            if (u1.Password == password && u1.IsAdminUser == true)
            {
                u1.ARegistr = true;
            }
            context.SaveChanges();
        }


    }

    public void AdminUserLogout()
    {
        var us = context.Users.FirstOrDefault(u => u.ARegistr == true);
        if (us is null) throw new DoesNotExistException("You need to log in as adminUser to log out");
        us.ARegistr = false;
        context.SaveChanges();


    }


}
