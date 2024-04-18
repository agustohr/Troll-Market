using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using TrollMarketWeb.ViewModels;

namespace TrollMarketWeb.APIGateway;

public class AuthConfigureAPI
{
    private readonly IConfiguration _configuration;

    public AuthConfigureAPI(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    private HttpClient ConfiguringHttpClient(){
        var client = new HttpClient();
        client.BaseAddress = new Uri(_configuration.GetValue<string>("API:BaseUrl"));
        return client;
    }

    public async Task<string> GetToken(AuthLoginViewModel viewModel){
        var client = ConfiguringHttpClient();
        var response = await client.PostAsJsonAsync($"/api/v1/auth/login", viewModel);
        try{
            var content = response.IsSuccessStatusCode ? await response.Content.ReadAsStringAsync() : throw new Exception(await response.Content.ReadAsStringAsync());
            return content;
        }catch(Exception exception){
            throw new Exception(exception.Message);
        }
    }

    private ClaimsPrincipal GetPrincipal(AuthLoginViewModel viewModel, string token){
        // var jsonToken = await GetToken(viewModel);
        // var token = JsonSerializer.Deserialize<ResponseToken>(jsonToken);
        var claims = new List<Claim>(){
            new Claim("username", viewModel.Username),
            new Claim(ClaimTypes.Role, viewModel.Role??""),
            new Claim("role", viewModel.Role??""),
            new Claim("token", token)
        };
        ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        return new ClaimsPrincipal(identity);
    }

    private AuthenticationTicket GetTicket(ClaimsPrincipal principal){
        AuthenticationProperties authenticationProperties = new AuthenticationProperties(){
            IssuedUtc = DateTime.Now,
            ExpiresUtc = DateTime.Now.AddMinutes(30),
            AllowRefresh = false
        };
        AuthenticationTicket authenticationTicket = new AuthenticationTicket(
            principal, authenticationProperties, CookieAuthenticationDefaults.AuthenticationScheme
        );
        return authenticationTicket;
    }

    public async Task<AuthenticationTicket> SetLogin(AuthLoginViewModel viewModel){
        try{
            var token = await GetToken(viewModel);
            ClaimsPrincipal principal = GetPrincipal(viewModel, token);
            AuthenticationTicket ticket = GetTicket(principal);
            return ticket;
        }catch(Exception exception){
            throw new Exception(exception.Message);
        }
    }

    public async Task<string> AddNewRegister(string role, AuthRegisterViewModel viewModel){
        viewModel.Role = role;
        var client = ConfiguringHttpClient();
        var response = await client.PostAsJsonAsync($"/api/v1/auth/register", viewModel);
        var content = response.IsSuccessStatusCode ? await response.Content.ReadAsStringAsync() : throw new Exception("Register Failed");
        return content;
    }

}
