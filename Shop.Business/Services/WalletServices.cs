﻿using Shop.Business.Interfaces;
using Shop.Business.Utilities.Exceptions;
using Shop.Core.Entities;
using Shop.DataAccess.DataAccess;

namespace Shop.Business.Services;

public class WalletServices : IWalletServices
{
    ShopDbContext context = new ShopDbContext();
    public void AddWallet(string userName,string cardNumber, decimal cardBalance )
    {
        if (string.IsNullOrEmpty(userName)) throw new ArgumentNullException();
        if (string.IsNullOrEmpty(cardNumber)) throw new ArgumentNullException();
        var wall = context.Wallets.FirstOrDefault(w => w.CardNumber == cardNumber);
        if (wall is not null) throw new AlreadyExistException($"Card with number {cardNumber} is already exist");

        Wallet wallet = new()
        {
            CardNumber = cardNumber,
            UserId = context.Users.First(u => u.Username == userName).Id,
            CardBalance = cardBalance
        };
        context.Wallets.Add( wallet );
        context.SaveChanges();
    }

    public void DeleteWallet(string userName, string password, int walletId)
    {
        if (string.IsNullOrEmpty(userName)) throw new ArgumentNullException();
        if (string.IsNullOrEmpty(password)) throw new ArgumentNullException();
        if (string.IsNullOrEmpty(walletId.ToString())) throw new ArgumentNullException();

        var user = context.Users.First(u => u.Username == userName);
        var wallet = context.Wallets.FirstOrDefault(w => w.Id == walletId);
        if (wallet is null) throw new DoesNotExistException($"Wallet with id :{walletId} doesn't exist,try again");
        if (wallet.UserId != user.Id) throw new IncorrectExeption($"Wallet with id :{walletId} doesn't belong to user with name :{userName}");
        context.Wallets.Remove( wallet );
        context.SaveChanges();


    }

    
}