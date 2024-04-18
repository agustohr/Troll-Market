using System.Globalization;
using TrollMarketBusiness.Interfaces;
using TrollMarketDataAccess.Models;
using static TrollMarketAPI.Constants;

namespace TrollMarketAPI.Cart;

public class CartService
{
    private readonly ITransactionRepository _repository;
    private readonly IAccountRepository _repositoryAccount;

    public CartService(ITransactionRepository repository, IAccountRepository accountRepository)
    {
        _repository = repository;
        _repositoryAccount = accountRepository;
    }

    public CartIndexDto Get(int pageNumber, string username){
        var dto = _repository.Get(pageNumber, PAGE_SIZE, username)
        .Select(cart => new CartDto(){
            Id = cart.Id,
            ProductName = cart.Product.Name,
            Quantity = cart.Quantity,
            ShipmentName = cart.Shipment.Name,
            SellerName = cart.Product.SellerUsernameNavigation.Name??"",
            TotalPrice = cart.TotalPrice.ToString("C2", CultureInfo.CreateSpecificCulture("id-ID"))
        });

        return new CartIndexDto(){
            Carts = dto.ToList(),
            Pagination = new PaginationDto(){
                PageNumber = pageNumber,
                PageSize = PAGE_SIZE,
                TotalItems = _repository.CountCarts(username)
            }
        };
    }

    public string PurchaseAll(string username){
        bool isCartExist = _repository.IsCartExist(username);
        if(!isCartExist){
            throw new Exception("Your cart is empty");
        }
        var cartToPurchase = _repository.Get(username);
        if(cartToPurchase.Count() == 0){
            throw new Exception("Sorry, product has been discontinued or shipment is not service");
            // throw new Exception("Sorry, the product you have in your cart has been discontinued");
        }
        decimal totalBill = 0; //buat ngurangin balance si buyer
        foreach (var cart in cartToPurchase){
            cart.PurchaseDate = DateTime.Now;
            totalBill += cart.TotalPrice;
        }

        var buyerAccount = _repositoryAccount.Get(username);
        if(buyerAccount.Balance >= totalBill){
            buyerAccount.Balance -= totalBill;
        }else{
            throw new Exception("Sorry, your balance is insufficient");
        }

        List<Account> SellersAccountToUpBalance = new List<Account>();
        var sellersIncomes = _repository.GetIncome(username);

        foreach(KeyValuePair<string,decimal> sellerIncome in sellersIncomes){
            var account = _repositoryAccount.Get(sellerIncome.Key);
            account.Balance += sellerIncome.Value;
            SellersAccountToUpBalance.Add(account);
        }


        _repository.UpdateListTransactions(cartToPurchase);
        _repositoryAccount.Update(buyerAccount);
        _repositoryAccount.UpdateListAccounts(SellersAccountToUpBalance);

        var totalCart = _repository.CountCarts(username);
        if(totalCart == 0){
            return "All products in the cart have been successfully purchased";
        }else{
            return "Some of the products in the basket have been successfully purchased,\n but there are several products that have failed to be purchased because the product has been discontinued";
        }
    }

    public void Remove(long id){
        var cart = _repository.GetById(id);
        _repository.Delete(cart);
    }
}
