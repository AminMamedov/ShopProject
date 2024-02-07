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
            if (us.URegistr == true)
            {

                var bas = context.Baskets.FirstOrDefault(b => b.UserId == userId);
                if (bas is null) throw new NotFoundException($"User with name {us.Username} doesn't have a basket");
                if (bas.ProductCount == 0) throw new DoesNotExistException("There is not any product in your basket");
                {
                    var basketProducts = context.BasketProducts.ToList();

                    foreach (var product in basketProducts)
                    {
                        if (product is not null && product.BasketID == bas.Id)
                        {
                            var pro = context.Products.FirstOrDefault(p => p.Id == product.ProductId);
                            if (pro != null && pro.IsActive == true)
                            {
                                Console.WriteLine($"Product Id:{pro.Id} ;product name: {pro.Name}; product price: {pro.Price}; product count : {product.ProductCount}");
                            }
                        }
                    }
                }
            }
        }
    }








    public void BuyProduct(int userId, int productId, int walletId, int count)
    {

        var us = context.Users.Find(userId);
        if (us is null) throw new DoesNotExistException($"User with Id: {userId} doesn't exist");
        if (us.URegistr == false) throw new NotLoggedInException("Please login to choose this operation");
        {
            var bas = context.Baskets.FirstOrDefault(b => b.UserId == userId);
            if (bas is not null)
            {
                var bp = context.BasketProducts.FirstOrDefault(bp => bp.ProductId == productId && bp.BasketID == bas.Id);
                if (bp is null) throw new DoesNotExistException($"Product with Id :{productId} doent't exist in your basket");
                var pro = context.Products.Find(productId);
                if (pro is null) throw new DoesNotExistException($"product with Id :{productId} doesn't exist");
                if (pro.ProductCount == 0) throw new DoesNotExistException($"product with Id :{productId} out of stock");
                {
                    var wal = context.Wallets.Find(walletId);
                    if (wal is null) throw new DoesNotExistException($"Wallet with Id:{walletId} doesn't exist");
                    if (wal.UserId != userId) throw new IncorrectExeption($"Wallet with Id:{walletId} doesn' belong to you");
                    if (pro.Price * count > wal.CardBalance) throw new TotalpriceMoreThanBalanceException($"You don't have enough money in card");

                    if (count > bp.ProductCount) throw new MoreThanBasProCountException($"This product's count in your basket = {bp.ProductCount}");
                    {

                        Invoice invoice = new()
                        {
                            UserId = userId,
                            ProductName = pro.Name,
                            ProductCount = count,
                            TotalPrice = count * pro.Price,
                            CreateTime = DateTime.Now
                        };
                        context.Invoices.Add(invoice);
                        context.SaveChanges();
                        ProductInvoice productInvoice = new()
                        {
                            ProductId = productId,
                            InvoiceId = invoice.Id,
                            ProductCount = count,
                            Price = invoice.TotalPrice

                        };
                        context.ProductInvoices.Add(productInvoice);
                        pro.ProductCount = pro.ProductCount - count;
                        bp.ProductCount = bp.ProductCount - count;
                        context.SaveChanges();
                        if (bp.ProductCount == 0)
                        {
                            context.BasketProducts.Remove(bp);
                        }
                        wal.CardBalance -= (count * pro.Price);
                        context.SaveChanges();
                        Console.WriteLine($"--------------------------------------\n" +
                                          $"----------------INVOICE---------------\n" +
                                          $"--------------------------------------\n" +
                                          $"Invoice Id :{invoice.Id}\n" +
                                          $"User Id : {invoice.UserId}\n" +
                                          $"Product name : {invoice.ProductName}\n" +
                                          $"Product count : {invoice.ProductCount}\n" +
                                          $"Total price : {invoice.TotalPrice}\n" +
                                          $"Created time : {invoice.CreateTime}\n" +
                                          $"--------------------------------------\n" +
                                          $"----------------INVOICE---------------\n" +
                                          $"--------------------------------------");

                    }
                }
            }


        }

    }
}

