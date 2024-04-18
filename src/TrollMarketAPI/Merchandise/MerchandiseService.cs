using System.Globalization;
using TrollMarketBusiness.Interfaces;
using TrollMarketDataAccess.Models;
using static TrollMarketAPI.Constants;

namespace TrollMarketAPI.Merchandise;

public class MerchandiseService
{
    private readonly IProductRepository _repository;

    public MerchandiseService(IProductRepository repository)
    {
        _repository = repository;
    }

    // public object Get(int pageNumber){
    //     var dto = _repository.Get(pageNumber, PAGE_SIZE)
    //     .Select(prod => new {
    //         Id = prod.Id,
    //         Name = prod.Name,
    //         Category = prod.Category,
    //         Discontinue = prod.IsDiscontinue ? "Yes" : "No"
    //     }).ToList();

    //     var pagination = new {
    //         PageNumber = pageNumber,
    //         PageSize = PAGE_SIZE,
    //         TotalItems = _repository.CountMerchandise()
    //     };

    //     return new { Merchandises = dto, Pagination = pagination };
    // }

    public MerchandiseIndexDto Get(int pageNumber, string username){
        var dto = _repository.Get(pageNumber, PAGE_SIZE, username)
        .Select(product => new MerchandiseDto(){
            Id = product.Id,
            Name = product.Name,
            Category = product.Category,
            Discontinue = product.IsDiscontinue ? "Yes" : "No"
        });

        return new MerchandiseIndexDto(){
            Merchandises = dto.ToList(),
            Pagination = new PaginationDto(){
                PageNumber = pageNumber,
                PageSize = PAGE_SIZE,
                TotalItems = _repository.CountMerchandise(username)
            }
        };
    }

    public void Insert(string username, MerchandiseInfoUpsertDto dto){
        var model = new Product(){
            Name = dto.Name,
            SellerUsername = username,
            Category = dto.Category,
            Description = dto.Description,
            Price = dto.Price,
            IsDiscontinue = dto.IsDiscontinue
        };
        _repository.Insert(model);
    }

    public void Update(long id, MerchandiseUpsertDto dto){
        var product = _repository.GetById(id);
        product.Name = dto.Name;
        product.Category = dto.Category;
        product.Description = dto.Description;
        product.Price = dto.Price;
        product.IsDiscontinue = dto.IsDiscontinue;

        _repository.Update(product);
    }

    public MerchandiseInfoUpsertDto Get(long id){
        var product = _repository.GetById(id);

        return new MerchandiseInfoUpsertDto(){
            Name = product.Name,
            Category = product.Category,
            Description = product.Description,
            Price = product.Price,
            PriceRp = product.Price.ToString("C2", CultureInfo.CreateSpecificCulture("id-ID")),
            IsDiscontinue = product.IsDiscontinue
        };
    }

    public void SetDiscontinue(long id){
        var product = _repository.GetById(id);
        product.IsDiscontinue = true;
        _repository.Update(product);
    }

    public bool CheckExistenceDependency(long id){
        return _repository.CheckDependency(id);
    }

    public void SoftDelete(long id){
        var product = _repository.GetById(id);
        product.IsDeleted = true;
        _repository.Update(product);
    }
}
