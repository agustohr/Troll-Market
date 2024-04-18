using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrollMarketWeb.APIGateway;
using TrollMarketWeb.ViewModels;

namespace TrollMarketWeb.Controllers;

[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    private readonly ConfigureAPI _gateway;

    public AdminController(ConfigureAPI configureAPI)
    {
        _gateway = configureAPI;
    }

    [HttpGet("admin")]
    public IActionResult Index(){
        return View(new NewAdminViewModel());
    }

    [HttpPost("admin")]
    public async Task<IActionResult> Add(NewAdminViewModel viewModel){
        if(ModelState.IsValid){
            string token = User.FindFirst("token")?.Value??"";
            string url = "/api/v1/admin";
            var result = await _gateway.PostAPI(viewModel, token, url);
            ViewBag.ResultMessage = result;
            return View("Index");
        }
        return View("Index", new NewAdminViewModel());
    }
}
