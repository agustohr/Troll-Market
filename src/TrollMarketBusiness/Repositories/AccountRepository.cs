using Microsoft.EntityFrameworkCore;
using TrollMarketBusiness.Interfaces;
using TrollMarketDataAccess.Models;

namespace TrollMarketBusiness.Repositories;

public class AccountRepository : IAccountRepository
{
    private readonly TrollMarketContext _dbContext;

    public AccountRepository(TrollMarketContext dbContext)
    {
        _dbContext = dbContext;
    }

    public List<Account> GetListSeller(){
        return _dbContext.Accounts.Where(account => account.Role == "Seller").ToList();
    }
    public List<Account> GetListBuyer(){
        return _dbContext.Accounts.Where(account => account.Role == "Buyer").ToList();
    }

    public Account Get(string username)
    {
        return _dbContext.Accounts.Find(username) ?? throw new Exception("Username or Password is incorrect");
    }

    // public Account GetWithHistory(int pageNumber, int pageSize, string username)
    // {
    //     return _dbContext.Accounts
    //     .Include(account => account.Transactions
    //         .Skip((pageNumber - 1) * pageSize)
    //         .Take(pageSize)
    //     )
    //         .ThenInclude(transaction => transaction.Product)
    //     .Include(account => account.Transactions
    //         .Skip((pageNumber - 1) * pageSize)
    //         .Take(pageSize)
    //     )
    //         .ThenInclude(transaction => transaction.Shipment)
    //     .Where(account => account.Username == username)
    //     .First();
    // }

    // public int CountUserHistories(string username){
    //     return (_dbContext.Accounts
    //     .Include(account => account.Transactions)
    //     .Where(account => account.Username == username)
    //     .First()).Transactions.Count();
    // }

    public void Insert(Account account)
    {
        _dbContext.Accounts.Add(account);
        _dbContext.SaveChanges();
    }

    public void Update(Account account)
    {
        _dbContext.Accounts.Update(account);
        _dbContext.SaveChanges();
    }

    public void UpdateListAccounts(List<Account> accounts)
    {
        _dbContext.Accounts.UpdateRange(accounts);
        _dbContext.SaveChanges();
    }
}
