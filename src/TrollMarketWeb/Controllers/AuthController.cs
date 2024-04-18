using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using TrollMarketWeb.APIGateway;
using TrollMarketWeb.ViewModels;

namespace TrollMarketWeb.Controllers;

public class AuthController : Controller
{
    private readonly AuthConfigureAPI _authGateway;

    public AuthController(AuthConfigureAPI authConfigureAPI)
    {
        _authGateway = authConfigureAPI;
    }

    [HttpGet()]
    public IActionResult Index(){
        return RedirectToAction("Login");
    }

    [HttpGet("login")]
    public IActionResult Login(){
        if(User?.Identity?.IsAuthenticated??false){
            return RedirectToAction("Index", "Home");
        }
        return View();
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout(){
        await HttpContext.SignOutAsync();
        return RedirectToAction("Index");
    }

    [HttpGet("register/{role}")]
    public IActionResult Register(string role){
        if(!(role == "Buyer" || role == "Seller")){
            return RedirectToAction("Index");
        }
        var viewModel = new AuthRegisterViewModel(){
            Role = role
        };
        return View("Register", viewModel);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(AuthLoginViewModel viewModel){
        if(ModelState.IsValid){
            try{
                var ticket = await _authGateway.SetLogin(viewModel);
                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme, 
                    ticket.Principal,
                    ticket.Properties
                );
                return RedirectToAction("Index", "Home");
            }catch(Exception exception){
                ViewBag.Message = exception.Message;
            }
        }
        return View();
    }

    [HttpPost("register/{role}")]
    public async Task<IActionResult> Register(string role, AuthRegisterViewModel viewModel){
        if(ModelState.IsValid){
            try{
                var result = await _authGateway.AddNewRegister(role, viewModel);
                ViewBag.Message = result;
                viewModel.Role = role;
                return View(viewModel);
            }catch(Exception exception){
                ViewBag.Message = exception.Message;
                viewModel.Role = role;
                return View(viewModel);
            }
        }
        viewModel.Role = role;
        return View(viewModel);
    }

    [HttpGet("AccessDenied")]
    public IActionResult AccessDenied(){
        return View();
    }
}
