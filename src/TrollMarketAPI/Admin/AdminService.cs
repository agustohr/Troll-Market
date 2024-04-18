using TrollMarketBusiness.Interfaces;
using TrollMarketDataAccess.Models;

namespace TrollMarketAPI.Admin;

public class AdminService
{
    private readonly IAccountRepository _repository;

    public AdminService(IAccountRepository repository)
    {
        _repository = repository;
    }

    public void NewAdmin(NewAdminDto dto){
        var model = new Account(){
            Username = dto.Username,
            Password = BCrypt.Net.BCrypt.HashPassword(dto.Password),
            Role = "Admin"
        };
        _repository.Insert(model);
    }
}
