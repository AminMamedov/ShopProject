namespace Shop.Business.Interfaces;

public interface IWalletServices
{
    void AddWallet(int userId, string bankName, string cardNumber, decimal cardBalance);
    void DeleteWallet(int userId, int walletId);
    
}
