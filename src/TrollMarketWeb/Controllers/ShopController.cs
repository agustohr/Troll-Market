using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TrollMarketWeb.APIGateway;
using TrollMarketWeb.ViewModels;

namespace TrollMarketWeb.Controllers;

[ApiController]
[Authorize(Roles = "Buyer")]
public class ShopController : Controller
{
    private readonly ConfigureAPI _gateway;

    public ShopController(ConfigureAPI configureAPI)
    {
        _gateway = configureAPI;
    }

    [HttpGet("shop")]
    public async Task<IActionResult> Index(int pageNumber = 1, string? productName = "", string? category = "", string? description = ""){
        string token = User.FindFirst("token")?.Value??"";
        string url = $"/api/v1/shop?pageNumber={pageNumber}&productName={productName}&category={category}&description={description}";
        var response = await _gateway.GetAPI(url, token);
        var viewModel = response.IsSuccessStatusCode ? await response.Content.ReadFromJsonAsync<ShopIndexViewModel>() : null;
        return View(viewModel);
    }

    [HttpGet("shop/detail/{id}")]
    public async Task<IActionResult> GetDetail(long id){
        string token = User.FindFirst("token")?.Value??"";
        string url = $"/api/v1/shop/detail/{id}";
        var response = await _gateway.GetAPI(url, token);
        var viewModel = response.IsSuccessStatusCode ? await response.Content.ReadFromJsonAsync<ShopDetailInfoViewModel>() : null;
        return Ok(viewModel);
    }

    [HttpGet("shop/selectlistshipments")]
    public async Task<IActionResult> GetSelectListShipment(long id){
        string token = User.FindFirst("token")?.Value??"";
        string url = "/api/v1/shop/selectlistshipments";
        var response = await _gateway.GetAPI(url, token);
        var viewModel = response.IsSuccessStatusCode ? await response.Content.ReadFromJsonAsync<List<SelectListItem>>() : null;
        return Ok(viewModel);
    }

    [HttpPost("shop/addcart/{id}")]
    public async Task<IActionResult> AddToCart(long id, ShopAddCartViewModel viewModel){
        try{
            var token = User.FindFirst("token")?.Value??"";
            string url = $"/api/v1/shop/addcart/{id}";
            var result = await _gateway.PostAPI(viewModel, token, url);
            return Ok(result);
        }catch(Exception exception){
            return Conflict(exception.Message);
        }
    }
}
