using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrollMarketWeb.APIGateway;
using TrollMarketWeb.ViewModels;

namespace TrollMarketWeb.Controllers;

[Authorize(Roles = "Admin")]
public class HistoryController : Controller
{
    private readonly ConfigureAPI _gateway;

    public HistoryController(ConfigureAPI configureAPI)
    {
        _gateway = configureAPI;
    }

    [HttpGet("history")]
    public async Task<IActionResult> Index(int pageNumber = 1, string? sellerUsername = "", string? buyerUsername = ""){
        var token = User.FindFirst("token")?.Value??"";
        var url = $"/api/v1/history?pageNumber={pageNumber}&sellerUsername={sellerUsername}&buyerUsername={buyerUsername}";
        var response = await _gateway.GetAPI(url, token);
        var viewModel = response.IsSuccessStatusCode ? await response.Content.ReadFromJsonAsync<HistoryIndexViewModel>() : null;
        return View(viewModel);
    }
}
