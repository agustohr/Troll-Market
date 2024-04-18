using TrollMarketDataAccess.Models;

namespace TrollMarketBusiness.Interfaces;

public interface IShipmentRepository
{
    List<Shipment> GetSelectList();
    List<Shipment> Get(int pageNumber, int pageSize);
    int CountShipments();
    Shipment GetById(long id);
    bool CheckDependency(long id);
    decimal GetPrice(long id);
    void Insert(Shipment shipment);
    void Update(Shipment shipment);
    void Delete(Shipment shipment);
}
