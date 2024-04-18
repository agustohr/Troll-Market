using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrollMarketWeb.APIGateway;

using TrollMarketWeb.ViewModels;

namespace TrollMarketWeb.Controllers;

[Authorize(Roles = "Buyer")]
public class CartController : Controller
{
    private readonly ConfigureAPI _gateway;

    public CartController(ConfigureAPI configureAPI)
    {
        _gateway = configureAPI;
    }

    [HttpGet("cart")]
    public async Task<IActionResult> Index(int pageNumber = 1){
        string token = User.FindFirst("token")?.Value??"";
        string url = $"/api/v1/cart?pageNumber={pageNumber}";
        var response = await _gateway.GetAPI(url, token);
        var viewModel = response.IsSuccessStatusCode ? await response.Content.ReadFromJsonAsync<CartIndexViewModel>() : null;
        return View(viewModel);
    }

    [HttpPut("cart/purchase")]
    public async Task<IActionResult> PurchaseAll(){
        string token = User.FindFirst("token")?.Value??"";
        string url = "/api/v1/cart/purchase";
        var response = await _gateway.PutAPI(new {}, token, url);
        return Ok(response);
    }

    [HttpDelete("cart/{id}")]
    public async Task<IActionResult> Remove(long id){
        string token = User.FindFirst("token")?.Value??"";
        string url = $"/api/v1/cart/{id}";
        var response = await _gateway.DeleteAPI(token, url);
        return Ok(response);
    }
}
