using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using TrollMarketBusiness.Interfaces;
using TrollMarketDataAccess.Models;

namespace TrollMarketAPI.Authorizations;

public class AuthService
{
    private readonly IAccountRepository _repository;
    private readonly IConfiguration _configuration;

    public AuthService(IAccountRepository repository, IConfiguration configuration)
    {
        _repository = repository;
        _configuration = configuration;
    }

    private AuthResponseDto CreateToken(Account model){
        List<Claim> claims = new List<Claim>(){
            new Claim(ClaimTypes.NameIdentifier, model.Username),
            new Claim(ClaimTypes.Role, model.Role)
        };
        
        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value)
        );

        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: credentials
        );

        var serializedToken = new JwtSecurityTokenHandler().WriteToken(token);

        return new AuthResponseDto(){
            Token = serializedToken
        };
    }
    public AuthResponseDto GetToken(AuthLoginDto request){
        var model = _repository.Get(request.Username);
        bool isCorrectPassword = BCrypt.Net.BCrypt.Verify(request.Password, model.Password);
        if(isCorrectPassword && model.Role == request.Role){
            return CreateToken(model);
        }
        throw new Exception("Username or Password Incorrect");
        
    }

    public void AddNewRegister(AuthRegisterDto viewModel){
        var model = new Account(){
            Username = viewModel.Username,
            Password = BCrypt.Net.BCrypt.HashPassword(viewModel.Password),
            Role = viewModel.Role,
            Name = viewModel.Name,
            Address = viewModel.Address,
            Balance = 0
        };
        _repository.Insert(model);
    }
}
