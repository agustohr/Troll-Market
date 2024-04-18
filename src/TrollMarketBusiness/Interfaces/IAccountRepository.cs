using TrollMarketDataAccess.Models;

namespace TrollMarketBusiness.Interfaces;

public interface IAccountRepository
{
    List<Account> GetListSeller();
    List<Account> GetListBuyer();
    Account Get(string username);
    // Account GetWithHistory(int pageNumber, int pageSize, string username);
    // int CountUserHistories(string username);
    void Insert(Account account);
    void Update(Account account);
    void UpdateListAccounts(List<Account> accounts);
}
