using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrollMarketWeb.APIGateway;
using TrollMarketWeb.ViewModels;

namespace TrollMarketWeb.Controllers;

[ApiController]
[Authorize(Roles = "Seller, Buyer")]
public class ProfileController : Controller
{
    private readonly ConfigureAPI _gateway;

    public ProfileController(ConfigureAPI configureAPI)
    {
        _gateway = configureAPI;
    }

    [HttpGet("profile")]
    public async Task<IActionResult> Index(int pageNumber = 1){
        string token = User.FindFirst("token")?.Value??"";
        string url = $"/api/v1/profile?pageNumber={pageNumber}";
        var response = await _gateway.GetAPI(url, token);
        var viewModel = response.IsSuccessStatusCode ? await response.Content.ReadFromJsonAsync<ProfileIndexViewModel>() : null;
        return View(viewModel);
    }

    [HttpPatch("profile")]
    public async Task<IActionResult> AddBalance(BalanceViewModel balanceModel){
        var token = User.FindFirst("token")?.Value??"";
        var url = "/api/v1/profile";
        var result = await _gateway.PatchAPI(balanceModel, token, url);
        return Ok(result);
    }
}
