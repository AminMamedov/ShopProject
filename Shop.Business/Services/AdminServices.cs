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
    public void UpdateProductDiscount(int? discountId, int? productId)
    {
        if (discountId is null) throw new ArgumentNullException();
        if (productId is null) throw new ArgumentNullException();
        var us = context.Users.FirstOrDefault(u => u.ARegistr == true);
        if (us is null) throw new NotLoggedInException("Login as Admin user to choose this operation ");
        else
        {
            var pro = context.Products.Find(productId);
            if (pro is null) throw new DoesNotExistException($"Product with Id :{productId} doesn't exist");
            var dis = context.Discounts.Find(discountId);
            if (dis is null) throw new DoesNotExistException($"Discount with Id :{discountId} doesn't exist");
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
    public void CreateCategory(string name)
    {
        var cat = context.Categories.FirstOrDefault(c => c.Name == name);
        if (cat is not null) throw new AlreadyExistException($"Category with name :{name} already exist");
        Category category = new()
        {
            Name = name,
            IsActive = true
        };
        context.Categories.Add(category);
        context.SaveChanges();
    }
    public void CreateBrand(string name)
    {
        var br = context.Brands.FirstOrDefault(b => b.Name == name);
        if (br is not null) throw new AlreadyExistException($"Brand with name :{name} already exist");
        Brand brand = new()
        {
            Name = name,
            IsActive = true
        };
        context.Brands.Add(brand);
        context.SaveChanges();
    }
    public void CreateProduct(string name, decimal price, int discountId, int proCount, int brandId, int categoryId)
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
                Price = price ,
                DiscountId = discountId,
                ProductCount = proCount,
                BrandId = brandId,
                CategoryId = categoryId
            };

                var dis = context.Discounts.Find(discountId);
            product.Price = price - (price * dis.Percentage) / 100;
            context.Products.Add(product);
            product.IsActive = true;
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
            pro.IsActive = false;
            context.SaveChanges();

        }


    }
    public void ShowDeactiveProducts()
    {
        foreach (var product in context.Products)
        {
            if (product.IsActive == false) 
            {
                Console.WriteLine($"Product ID :{product.Id} // Product name: {product.Name} // Product price :{product.Price} // Product count :{product.ProductCount}");
            }
        }
    }
    public void ActivateProduct(int productId)
    {

        var pro = context.Products.Find(productId);
        if (pro is null) throw new DoesNotExistException($"Product with id: {productId} doesn't exist");
        if (pro.IsActive == true) throw new AlreadyActiveException($"Product with Id :{productId} is already active");
        pro.IsActive = true;
        context.SaveChanges();
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
            foreach (var product in context.Products)
            {
                if (product.DiscountId == discountId)
                {
                    decimal perc = (100 - dis.Percentage);
                    product.Price = (product.Price * 100) / perc;

                }
            }
            dis.IsActive = false;
            context.SaveChanges();

        }
    }
    public void ShowAllDiscounts()
    {
        var us = context.Users.FirstOrDefault(u => u.ARegistr == true);
        if (us is null) throw new NotLoggedInException("Login as Admin user to choose this operation ");
        foreach (var dis in context.Discounts)
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
            if (product.IsActive == true)
            {

                if (product.DiscountId is null)
                {
                    Console.WriteLine($"Product Id :{product.Id} ; product name:{product.Name} ; product price: {product.Price}");

                }
                else
                {
                    Console.WriteLine($"Product Id :{product.Id} ; product name:{product.Name} ; product price: {product.Price} ; product discountId:{product.DiscountId}");

                }
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
    public void ShowAllCategory()
    {
        foreach (var category in context.Categories)
        {
            if (category is not null && category.IsActive == true)
            {
                Console.WriteLine($"Category id: {category.Id}; category name: {category.Name}");
            }
        }
    }
    public void ShowAllBrands()
    {
        foreach (var brand in context.Brands)
        {
            if (brand is not null && brand.IsActive == true)
            {
                Console.WriteLine($"Brand id: {brand.Id}; Brand name: {brand.Name}");
            }
        }
    }
    public void AdminUserLogin(string userName, string password)
    {
        if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password)) throw new ArgumentException();
        var u1 = context.Users.FirstOrDefault(u => u.Username == userName);
        if (u1 is null) throw new DoesNotExistException($"Admin user with name:{userName} doesn't exist");
        if (u1.Password != password) throw new IncorrectExeption("Username or password is incorrect,please try again");

        if (u1.IsAdminUser == true)
        {
            u1.ARegistr = true;
        }
        context.SaveChanges();



    }

    public void AdminUserLogout()
    {
        var us = context.Users.FirstOrDefault(u => u.ARegistr == true);
        if (us is null) throw new DoesNotExistException("You need to log in as adminUser to log out");
        us.ARegistr = false;
        context.SaveChanges();


    }


}
