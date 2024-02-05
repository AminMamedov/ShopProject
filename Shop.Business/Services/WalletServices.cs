using Shop.Business.Interfaces;
using Shop.Business.Utilities.Exceptions;
using Shop.Core.Entities;
using Shop.DataAccess.DataAccess;

namespace Shop.Business.Services;

public class WalletServices : IWalletServices
{
    #region UserMethods

    private ShopDbContext context { get; }
    public WalletServices()
    {
        context = new ShopDbContext();
    }
    public void AddWallet(int userId, string cardNumber, decimal cardBalance)
    {
        var us = context.Users.Find(userId);
        if (us is null) throw new DoesNotExistException($"User with id:{userId} doesn't exist");
        if (us.URegistr == true)
        {

            if (string.IsNullOrEmpty(cardNumber)) throw new ArgumentNullException();
            var wall = context.Wallets.FirstOrDefault(w => w.CardNumber == cardNumber);
            if (wall is not null) throw new AlreadyExistException($"Card with number {cardNumber} is already exist");

            Wallet wallet = new()
            {
                CardNumber = cardNumber,
                UserId = us.Id,
                CardBalance = cardBalance
            };
            context.Wallets.Add(wallet);
            context.SaveChanges();


        }
    }

    public void DeleteWallet(int userId,  int walletId)
    {
        var us = context.Users.Find(userId);
        if (us is null) throw new DoesNotExistException($"User with id:{userId} doesn't exist");        
        if (string.IsNullOrEmpty(walletId.ToString())) throw new ArgumentNullException();

   
        var wallet = context.Wallets.FirstOrDefault(w => w.Id == walletId);
        if (wallet is null) throw new DoesNotExistException($"Wallet with id :{walletId} doesn't exist,try again");
        if (wallet.UserId != userId) throw new IncorrectExeption($"Wallet with id :{walletId} doesn't belong to user with name :{us.FirsName}");
        context.Wallets.Remove(wallet);
        context.SaveChanges();


    }

    #endregion



}
