namespace Shop.Business.Interfaces;

public interface IWalletServices
{
    void AddWallet(string userName, string cardNumber, decimal cardBalance);
    void DeleteWallet(string userName, string password, int walletId);
    
}
