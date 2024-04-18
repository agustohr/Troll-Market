using System.Globalization;
using Microsoft.AspNetCore.Mvc.Rendering;
using TrollMarketBusiness.Interfaces;
using TrollMarketDataAccess.Models;
using static TrollMarketAPI.Constants;

namespace TrollMarketAPI.Shop;

public class ShopService
{
    private readonly IProductRepository _repository;
    private readonly ITransactionRepository _repositoryTransaction;
    private readonly IShipmentRepository _repositoryShipment;

    public ShopService(IProductRepository repository, ITransactionRepository transactionRepository, IShipmentRepository shipmentRepository)
    {
        _repository = repository;
        _repositoryTransaction = transactionRepository;
        _repositoryShipment = shipmentRepository;
    }

    public ShopIndexDto Get(int pageNumber, string? productName, string? category, string? description){
        var dto = _repository.Get(pageNumber, PAGE_SIZE, productName, category, description)
        .Select(product => new ShopDto(){
            Id = product.Id,
            ProductName = product.Name,
            Price = product.Price.ToString("C2", CultureInfo.CreateSpecificCulture("id-ID"))
        });

        return new ShopIndexDto(){
            ShopItems = dto.ToList(),
            Pagination = new PaginationDto(){
                PageNumber = pageNumber,
                PageSize = PAGE_SIZE,
                TotalItems = _repository.CountShopItems(productName, category, description)
            }
        };
    }

    public ShopDetailDto Get(long id){
        var product = _repository.GetById(id);

        return new ShopDetailDto(){
            Name = product.Name,
            Category = product.Category,
            Description = product.Description,
            Price = product.Price.ToString("C2", CultureInfo.CreateSpecificCulture("id-ID")),
            SellerName = product.SellerUsernameNavigation.Name??""
        };
    }

    public void AddToCart(long id, string buyerUsername, ShopAddCartDto dto){
        var cartChecked = _repositoryTransaction.CheckExistProductInCart(id, dto.ShipmentId, buyerUsername);
        decimal productPrice = _repository.GetPrice(id);
        if(cartChecked == null){
            decimal shipmentPrice = _repositoryShipment.GetPrice(dto.ShipmentId);
            var model = new Transaction(){
                ProductId = id,
                ShipmentId = dto.ShipmentId,
                BuyerUsername = buyerUsername,
                Quantity = dto.Quantity,
                TotalPrice = (dto.Quantity * productPrice) + shipmentPrice
            };
            _repositoryTransaction.Insert(model);
        }else{
            cartChecked.Quantity += dto.Quantity;
            cartChecked.TotalPrice += dto.Quantity * productPrice;
            _repositoryTransaction.Update(cartChecked);
        }
    }

    public List<SelectListItem> GetSelectListShipment(){
        return _repositoryShipment.GetSelectList()
        .Select(shipment => new SelectListItem(){
            Text = shipment.Name,
            Value = shipment.Id.ToString()
        }).ToList();
    }
}
