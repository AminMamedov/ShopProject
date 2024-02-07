using Shop.Business.Interfaces;
using Shop.Core.Entities;
using Shop.DataAccess.DataAccess;
using Shop.Business.Utilities.Exceptions;

namespace Shop.Business.Services;

public class ProductServices : IProductServices
{
    private ShopDbContext context { get; }
    public ProductServices()
    {
        context = new ShopDbContext();
    }

    #region userMethods

    #endregion
    public void AddProductToBasket(int productId, int userId, int proCount)
    {
        if (productId < 0) { throw new ArgumentOutOfRangeException(); }
        if (proCount < 0) { throw new ArgumentOutOfRangeException(); }
        var pro = context.Products.Find(productId);
        if (pro is null) throw new NotFoundException($"Product with id :{productId} doesn't exist");
        if (proCount > pro.ProductCount) throw new MoreThanStockCountException($"The product count in stock = {pro.ProductCount}");
        var us = context.Users.Find(userId);
        var bas = context.Baskets.FirstOrDefault(b => b.UserId == userId);
        if (bas is null) throw new DoesNotExistException("This user doesn't have any basket");
        if (us == null) throw new NotFiniteNumberException($"User with Id :{userId} not found");
        if (us.URegistr == true)
        {
            var basproduct = context.BasketProducts.FirstOrDefault(b => b.ProductId == productId && b.BasketID == bas.Id);
            if (basproduct is not null)
            {
                basproduct.ProductCount = basproduct.ProductCount + proCount;
                bas.ProductCount = bas.ProductCount + proCount;
                context.SaveChanges();
            }
            else
            {
                BasketProduct basketProduct = new()
                {
                    ProductId = productId,
                    BasketID = bas.Id,
                    ProductCount = proCount,

                };
                context.BasketProducts.Add(basketProduct);
                bas.ProductCount = bas.ProductCount + proCount;
                context.SaveChanges();

            }


        }
    }

    public void DeleteProductFromBasket(int productId, int userId, int proCount)
    {
        if (productId < 0) { throw new ArgumentOutOfRangeException(); }
        if (proCount < 0) { throw new ArgumentOutOfRangeException(); }
        var pro = context.Products.Find(productId);
        if (pro is null) throw new NotFoundException($"Product with id :{productId} doesn't exist");
        var us = context.Users.Find(userId);
        var bas = context.Baskets.FirstOrDefault(b => b.UserId == userId);
        if (bas is null) throw new DoesNotExistException("This user doesn't have any basket");
        if (us == null) throw new NotFiniteNumberException($"User with Id :{userId} not found");
        var bp = context.BasketProducts.FirstOrDefault(bp => bp.ProductId == productId && bp.BasketID == bas.Id);
        if (bp is null) throw new DoesNotExistException($"Product with Id :{productId} doesn't exist in your basket");
        if (proCount > bp.ProductCount) throw new MoreThanBasProCountException($"This product's count in your basket  = {bp.ProductCount}");
        if (us.URegistr == true)
        {
            bas.ProductCount = bas.ProductCount - proCount;
            bp.ProductCount = bp.ProductCount - proCount;
            context.SaveChanges();
            if (bp.ProductCount == 0)
            {
                context.BasketProducts.Remove(bp);
                context.SaveChanges();
            }
        }
    }



    public void ShowAllProducts()
    {
        foreach (var product in context.Products)
        {
            if (product.DiscountId is null && product.IsActive == true)
            {
                Console.WriteLine($"Product Id :{product.Id} ; product name:{product.Name} ; product price: {product.Price} ; product count:{product.ProductCount}; ");

            }
            if (product.DiscountId is not null && product.IsActive == true)
            {
                Console.WriteLine($"Product Id :{product.Id} ; product name:{product.Name} ; product price: {product.Price} ; product count:{product.ProductCount}; product discountId:{product.DiscountId}");

            }



        }
    }
    #region adminUser Methods









    #endregion
}
