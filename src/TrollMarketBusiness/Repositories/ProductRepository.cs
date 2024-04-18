using Microsoft.EntityFrameworkCore;
using TrollMarketBusiness.Interfaces;
using TrollMarketDataAccess.Models;

namespace TrollMarketBusiness.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly TrollMarketContext _dbContext;

    public ProductRepository(TrollMarketContext dbContext)
    {
        _dbContext = dbContext;
    }

    public List<Product> Get(int pageNumber, int pageSize, string username)
    {
        return _dbContext.Products
        .Where(product => product.IsDeleted == false && product.SellerUsername == username)
        .Skip((pageNumber - 1) * pageSize)
        .Take(pageSize)
        .ToList();
    }

    public List<Product> Get(int pageNumber, int pageSize, string? productName, string? category, string? description){
        return _dbContext.Products
        .Where(product => 
            product.IsDeleted == false && 
            product.IsDiscontinue == false &&
            product.Name.ToLower().Contains(productName??"".ToLower()) &&
            product.Category.ToLower().Contains(category??"".ToLower()) &&
            (product.Description??"").ToLower().Contains(description??"".ToLower())
        )
        .Skip((pageNumber - 1) * pageSize)
        .Take(pageSize)
        .ToList();
    }

    public int CountMerchandise(string username)
    {
        return _dbContext.Products
        .Where(product => product.IsDeleted == false && product.SellerUsername == username)
        .Count();
    }

    public int CountShopItems(string? productName, string? category, string? description)
    {
        return _dbContext.Products
        .Where(product => 
            product.IsDeleted == false && 
            product.IsDiscontinue == false &&
            product.Name.ToLower().Contains(productName??"".ToLower()) &&
            product.Category.ToLower().Contains(category??"".ToLower()) &&
            (product.Description??"").ToLower().Contains(description??"".ToLower())
        )
        .Count();
    }

    public Product GetById(long id)
    {
        // return _dbContext.Products.Find(id) ?? throw new NullReferenceException($"Product with id {id} is not found");
        return _dbContext.Products
        .Include(product => product.SellerUsernameNavigation)
        .Where(product => product.Id == id)
        .First();
    }

    public decimal GetPrice(long id)
    {
        return _dbContext.Products.Find(id)?.Price??0;
    }

    public bool CheckDependency(long id){
        var check = (_dbContext.Products
        .Include(product => product.Transactions)
        .Where(product => product.Id == id)
        .First()).Transactions.Count();
        return check == 0 ? false : true;
    }

    public void Insert(Product product)
    {
        _dbContext.Products.Add(product);
        _dbContext.SaveChanges();
    }

    public void Update(Product product)
    {
        _dbContext.Products.Update(product);
        _dbContext.SaveChanges();
    }

    public void Delete(Product product)
    {
        _dbContext.Products.Remove(product);
        _dbContext.SaveChanges();
    }

}
