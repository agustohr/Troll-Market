using TrollMarketDataAccess.Models;

namespace TrollMarketBusiness.Interfaces;

public interface IProductRepository
{
    List<Product> Get(int pageNumber, int pageSize, string username);
    List<Product> Get(int pageNumber, int pageSize, string? productName, string? category, string? description);
    int CountMerchandise(string username);
    int CountShopItems(string? productName, string? category, string? description);
    Product GetById(long id);
    decimal GetPrice(long id);
    bool CheckDependency(long id);
    void Insert(Product product);
    void Update(Product product);
    void Delete(Product product);
}
