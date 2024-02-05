namespace Shop.Business.Interfaces;

public interface IWalletServices
{
    void AddWallet(int userId, string cardNumber, decimal cardBalance);
    void DeleteWallet(int userId, string password, int walletId);
    
}
