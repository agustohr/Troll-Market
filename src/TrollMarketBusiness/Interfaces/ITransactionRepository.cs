using TrollMarketDataAccess.Models;

namespace TrollMarketBusiness.Interfaces;

public interface ITransactionRepository
{
    List<Transaction> Get(int pageNumber, int pageSize, string? sellerUsername, string? buyerUsername);
    List<Transaction> GetUserHistories(int pageNumber, int pageSize, string? username);
    int CountUserHistories(string username);
    List<Transaction> Get(string buyerUsername);
    bool IsCartExist(string buyerUsername);
    Dictionary<string,decimal> GetIncome(string buyerUsername);
    List<Transaction> Get(int pageNumber, int pageSize, string buyerUsername);
    int CountCarts(string buyerUsername);
    int CountHistories(string? sellerUsername, string? buyerUsername);
    Transaction GetById(long id);
    Transaction? CheckExistProductInCart(long productId, long shipmentId, string buyerUsername);
    void Insert(Transaction transaction);
    void Update(Transaction transaction);
    void UpdateListTransactions(List<Transaction> cartToPurchase);
    void Delete(Transaction transaction);
}
