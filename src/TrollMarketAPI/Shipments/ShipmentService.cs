using System.Globalization;
using TrollMarketBusiness.Interfaces;
using TrollMarketDataAccess.Models;
using static TrollMarketAPI.Constants;

namespace TrollMarketAPI.Shipments;

public class ShipmentService
{
    private readonly IShipmentRepository _repository;

    public ShipmentService(IShipmentRepository repository)
    {
        _repository = repository;
    }
    
    public ShipmentIndexDto Get(int pageNumber){
        var dto = _repository.Get(pageNumber, PAGE_SIZE)
        .Select(shipment => new ShipmentDto(){
            Id = shipment.Id,
            Name = shipment.Name,
            Price = shipment.Price,
            PriceRp = shipment.Price.ToString("C2", CultureInfo.CreateSpecificCulture("id-ID")),
            IsService = shipment.IsService
        });

        return new ShipmentIndexDto(){
            Shipments = dto.ToList(),
            Pagination = new PaginationDto(){
                PageNumber = pageNumber,
                PageSize = PAGE_SIZE,
                TotalItems = _repository.CountShipments()
            }
        };
    }

    public ShipmentDto Get(long id){
        var shipment = _repository.GetById(id);

        return new ShipmentDto(){
            Id = shipment.Id,
            Name = shipment.Name,
            Price = shipment.Price,
            IsService = shipment.IsService
        };
    }

    public void Insert(ShipmentDto dto){
        var model = new Shipment(){
            Name = dto.Name,
            Price = dto.Price,
            IsService = dto.IsService
        };
        _repository.Insert(model);
    }

    public void Update(long id, ShipmentDto dto){
        var shipment = _repository.GetById(id);
        shipment.Name = dto.Name;
        shipment.Price = dto.Price;
        shipment.IsService = dto.IsService;

        _repository.Update(shipment);
    }

    public void StopService(long id){
        var shipment = _repository.GetById(id);
        shipment.IsService = false;
        _repository.Update(shipment);
    }

    public void SoftDelete(long id){
        var shipment = _repository.GetById(id);
        shipment.IsDeleted = true;
        _repository.Update(shipment);
    }

    public bool CheckTransaction(long id){
        return _repository.CheckDependency(id);
    }
}
