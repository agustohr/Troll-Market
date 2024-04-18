using System.Globalization;
using TrollMarketBusiness.Interfaces;
using static TrollMarketAPI.Constants;

namespace TrollMarketAPI.Profile;

public class ProfileService
{
    private readonly IAccountRepository _repositoryAccount;
    private readonly ITransactionRepository _repositoryTransaction;

    public ProfileService(IAccountRepository repositoryAccount, ITransactionRepository repositoryTransaction)
    {
        _repositoryAccount = repositoryAccount;
        _repositoryTransaction = repositoryTransaction;
    }

    public ProfileIndexDto Get(int pageNumber, string username){
        var user = _repositoryAccount.Get(username);

        var userHistory = _repositoryTransaction.GetUserHistories(pageNumber, PAGE_SIZE, username)
        .Select(history => new HistoryUserDto(){
            PurchaseDate = history.PurchaseDate?.ToString("dd/MM/yyyy")??"",
            ProductName = history.Product.Name,
            Quantity = history.Quantity,
            ShipmentName = history.Shipment.Name,
            TotalPrice = user.Role == "Seller" ? (history.TotalPrice - history.Shipment.Price).ToString("C2", CultureInfo.CreateSpecificCulture("id-ID")) : history.TotalPrice.ToString("C2", CultureInfo.CreateSpecificCulture("id-ID"))
        });



        // var userHistory = user.Transactions.Select(history => new HistoryUserDto(){
        //     PurchaseDate = history.PurchaseDate?.ToString("dd/MM/yyyy")??"",
        //     ProductName = history.Product.Name,
        //     Quantity = history.Quantity,
        //     ShipmentName = history.Shipment.Name,
        //     TotalPrice = history.TotalPrice
        // });

        // var user = _repositoryAccount.Get(username);
        // var userHistory = _repositoryTransaction.

        return new ProfileIndexDto(){
            Name = user.Name??"",
            Role = user.Role,
            Address = user.Address??"",
            Balance = user.Balance?.ToString("C2", CultureInfo.CreateSpecificCulture("id-ID"))??"-",
            UserHistories = userHistory.ToList(),
            Pagination = new PaginationDto(){
                PageNumber = pageNumber,
                PageSize = PAGE_SIZE,
                TotalItems = _repositoryTransaction.CountUserHistories(username)
            }
        };
    }

    public void AddBalance(string username, BalanceDto balance){
        var account = _repositoryAccount.Get(username);
        account.Balance = account.Balance + balance.Balance;
        _repositoryAccount.Update(account);
    }
}
