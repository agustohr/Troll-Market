using Microsoft.EntityFrameworkCore;
using TrollMarketBusiness.Interfaces;
using TrollMarketDataAccess.Models;

namespace TrollMarketBusiness.Repositories;

public class TransactionRepository : ITransactionRepository
{
    private readonly TrollMarketContext _dbContext;

    public TransactionRepository(TrollMarketContext dbContext)
    {
        _dbContext = dbContext;
    }

    public List<Transaction> Get(int pageNumber, int pageSize, string? sellerUsername, string? buyerUsername){
        return _dbContext.Transactions
        .Include(transaction => transaction.Product)
            .ThenInclude(product => product.SellerUsernameNavigation)
        .Include(transaction => transaction.BuyerUsernameNavigation)
        .Include(transaction => transaction.Shipment)
        .Where(transaction => 
            transaction.PurchaseDate != null && 
            ((buyerUsername != null) ? (transaction.BuyerUsername == buyerUsername) : true) &&
            ((sellerUsername != null) ? (transaction.Product.SellerUsername) == sellerUsername : true)
        )
        .Skip((pageNumber - 1) * pageSize)
        .Take(pageSize)
        .ToList();
    }

    public List<Transaction> GetUserHistories(int pageNumber, int pageSize, string? username){
        var result = _dbContext.Transactions
        .Include(transaction => transaction.Product)
        .Include(transaction => transaction.Shipment)
        .Where(transaction => 
            transaction.PurchaseDate != null && 
            (transaction.BuyerUsername == username || 
            transaction.Product.SellerUsername == username)
        )
        .Skip((pageNumber - 1) * pageSize)
        .Take(pageSize)
        .ToList();
        return result;
    }

    public int CountUserHistories(string username){
        return _dbContext.Transactions
        .Include(transaction => transaction.Product)
        .Where(transaction => 
            transaction.PurchaseDate != null && 
            (transaction.BuyerUsername == username || 
            transaction.Product.SellerUsername == username)
        )
        .Count();
    }

    public List<Transaction> Get(string buyerUsername){
        return _dbContext.Transactions
        .Include(t => t.Product)
        .Where(t => 
            t.Product.IsDiscontinue == false &&
            t.Shipment.IsService == true &&
            t.PurchaseDate == null && 
            t.BuyerUsername == buyerUsername
        ).ToList();
    }
    public bool IsCartExist(string buyerUsername){
        return _dbContext.Transactions.Any(t => t.PurchaseDate == null && t.BuyerUsername == buyerUsername);
    }

    public Dictionary<string,decimal> GetIncome(string buyerUsername){
        var result = from t in _dbContext.Transactions
            join p in _dbContext.Products on t.ProductId equals p.Id
            join a in _dbContext.Accounts on p.SellerUsername equals a.Username
            join s in _dbContext.Shipments on t.ShipmentId equals s.Id
            where t.PurchaseDate == null && t.BuyerUsername == buyerUsername && p.IsDiscontinue == false
            group new { a, t, p, s } by a.Username into g
            select new {
                SellerName = g.Key,
                Income = g.Sum(x => (x.t.TotalPrice - x.s.Price))
            };
            // select new Dictionary<string,decimal>(){
            //     {g.Key, g.Sum(x => (x.t.TotalPrice - x.s.Price))}
            //     // SellerName = g.Key,
            //     // Income = g.Sum(x => (x.t.TotalPrice - x.s.Price))
            // };
        return result.ToDictionary(x=>x.SellerName, x=>x.Income);
        // return result;
    }

    public List<Transaction> Get(int pageNumber, int pageSize, string buyerUsername)
    {
        return _dbContext.Transactions
        .Include(transaction => transaction.Product)
            .ThenInclude(product => product.SellerUsernameNavigation)
        .Include(transaction => transaction.Shipment)
        .Where(transaction => transaction.PurchaseDate == null && transaction.BuyerUsername == buyerUsername)
        .Skip((pageNumber - 1) * pageSize)
        .Take(pageSize)
        .ToList();
    }
    public int CountCarts(string buyerUsername)
    {
        return _dbContext.Transactions
        // .Include(transaction => transaction.Product)
        //     .ThenInclude(product => product.SellerUsernameNavigation)
        // .Include(transaction => transaction.Shipment)
        .Where(transaction => transaction.PurchaseDate == null && transaction.BuyerUsername == buyerUsername)
        .Count();
    }

    public int CountHistories(string? sellerUsername, string? buyerUsername)
    {
        return _dbContext.Transactions
        // .Include(transaction => transaction.Product)
        //     .ThenInclude(product => product.SellerUsernameNavigation)
        // .Include(transaction => transaction.BuyerUsernameNavigation)
        // .Include(transaction => transaction.Shipment)
        .Where(transaction => 
            transaction.PurchaseDate != null && 
            ((buyerUsername != null) ? (transaction.BuyerUsername == buyerUsername) : true) &&
            ((sellerUsername != null) ? (transaction.Product.SellerUsername) == sellerUsername : true)
        )
        .Count();
    }

    public Transaction GetById(long id)
    {
        return _dbContext.Transactions.Find(id) ?? throw new NullReferenceException($"Transaction with Id {id} is not found");
    }

    public Transaction? CheckExistProductInCart(long productId, long shipmentId, string buyerUsername){
        var result = _dbContext.Transactions
        .Where(transaction => 
            transaction.ProductId == productId && 
            transaction.ShipmentId == shipmentId &&
            transaction.BuyerUsername == buyerUsername &&
            transaction.PurchaseDate == null
        ).FirstOrDefault();
        return result;
    }

    public void Insert(Transaction transaction)
    {
        _dbContext.Transactions.Add(transaction);
        _dbContext.SaveChanges();
    }

    public void Update(Transaction transaction){
        _dbContext.Transactions.Update(transaction);
        _dbContext.SaveChanges();
    }

    public void UpdateListTransactions(List<Transaction> cartToPurchase)
    {
        _dbContext.UpdateRange(cartToPurchase);
        _dbContext.SaveChanges();
    }

    public void Delete(Transaction transaction)
    {
        _dbContext.Transactions.Remove(transaction);
        _dbContext.SaveChanges();
    }
}
