using Microsoft.EntityFrameworkCore;
using TrollMarketBusiness.Interfaces;
using TrollMarketDataAccess.Models;

namespace TrollMarketBusiness.Repositories;

public class ShipmentRepository : IShipmentRepository
{
    private readonly TrollMarketContext _dbContext;

    public ShipmentRepository(TrollMarketContext dbContext)
    {
        _dbContext = dbContext;
    }

    public List<Shipment> GetSelectList(){
        return _dbContext.Shipments
        .Where(shipment => 
            shipment.IsDeleted == false &&
            shipment.IsService == true
        ).ToList();
    }

    public List<Shipment> Get(int pageNumber, int pageSize)
    {
        return _dbContext.Shipments
        .Where(shipment => shipment.IsDeleted == false)
        .Skip((pageNumber - 1) * pageSize)
        .Take(pageSize)
        .ToList();
    }

    public int CountShipments()
    {
        return _dbContext.Shipments
        .Where(shipment => shipment.IsDeleted == false)
        .Count();
    }

    public Shipment GetById(long id)
    {
        return _dbContext.Shipments.Find(id) ?? throw new NullReferenceException($"Shipment with id {id} is not found");
    }

    public bool CheckDependency(long id){
        // var query = _dbContext.Shipments
        // .Include(shipment => shipment.Transactions)

        var check = (_dbContext.Shipments
        .Include(shipment => shipment.Transactions)
        .Where(shipment => shipment.Id == id)
        .First()).Transactions.Count();
        return check == 0 ? false : true;
    }

    public decimal GetPrice(long id)
    {
        return _dbContext.Shipments.Find(id)?.Price??0;
    }

    public void Insert(Shipment shipment)
    {
        _dbContext.Shipments.Add(shipment);
        _dbContext.SaveChanges();
    }

    public void Update(Shipment shipment)
    {
        _dbContext.Shipments.Update(shipment);
        _dbContext.SaveChanges();
    }

    public void Delete(Shipment shipment)
    {
        _dbContext.Shipments.Remove(shipment);
        _dbContext.SaveChanges();
    }
}
