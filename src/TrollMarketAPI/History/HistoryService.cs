using System.Globalization;
using Microsoft.AspNetCore.Mvc.Rendering;
using TrollMarketBusiness.Interfaces;
using static TrollMarketAPI.Constants;

namespace TrollMarketAPI.History;

public class HistoryService
{
    private readonly ITransactionRepository _repository;
    private readonly IAccountRepository _repositoryAccount;

    public HistoryService(ITransactionRepository repository, IAccountRepository accountRepository)
    {
        _repository = repository;
        _repositoryAccount = accountRepository;
    }

    public List<SelectListItem> GetSelectListSeller(){
        return _repositoryAccount.GetListSeller()
            .Select(account=>new SelectListItem(){
                Text = account.Name,
                Value = account.Username
            }).ToList();
    }

    public List<SelectListItem> GetSelectListBuyer(){
        return _repositoryAccount.GetListBuyer()
            .Select(account=>new SelectListItem(){
                Text = account.Name,
                Value = account.Username
            }).ToList();
    }

    public HistoryIndexDto Get(int pageNumber, string? sellerUsername, string? buyerUsername){
        var dto = _repository.Get(pageNumber, PAGE_SIZE, sellerUsername, buyerUsername)
        .Select(transaction => new HistoryDto(){
            PurchaseDate = transaction.PurchaseDate?.ToString("dd/MM/yyyy")??"",
            SellerName = transaction.Product.SellerUsernameNavigation.Name??"",
            BuyerName = transaction.BuyerUsernameNavigation.Name??"",
            ProductName = transaction.Product.Name,
            Quantity = transaction.Quantity,
            ShipmentName = transaction.Shipment.Name,
            TotalPrice = transaction.TotalPrice.ToString("C2", CultureInfo.CreateSpecificCulture("id-ID"))
        });

        return new HistoryIndexDto(){
            Histories = dto.ToList(),
            Pagination = new PaginationDto(){
                PageNumber = pageNumber,
                PageSize = PAGE_SIZE,
                TotalItems = _repository.CountHistories(sellerUsername, buyerUsername)
            },
            Seller = sellerUsername,
            Buyer = buyerUsername,
            Sellers = GetSelectListSeller(),
            Buyers = GetSelectListBuyer()
        };
    }
}
