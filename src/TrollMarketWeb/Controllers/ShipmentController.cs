using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrollMarketWeb.APIGateway;
using TrollMarketWeb.ViewModels;

namespace TrollMarketWeb.Controllers;

[ApiController]
[Authorize(Roles = "Admin")]
public class ShipmentController : Controller
{
    private readonly ConfigureAPI _gateway;

    public ShipmentController(ConfigureAPI configureAPI)
    {
        _gateway = configureAPI;
    }
    
    [HttpGet("shipment")]
    public async Task<IActionResult> Index(int pageNumber = 1){
        string token = User.FindFirst("token")?.Value??"";
        string url = $"/api/v1/shipment?pageNumber={pageNumber}";
        var response = await _gateway.GetAPI(url, token);
        var viewModel = response.IsSuccessStatusCode ? await response.Content.ReadFromJsonAsync<ShipmentIndexViewModel>() : null;
        return View(viewModel);
    }

    [HttpGet("shipment/{id}")]
    public async Task<IActionResult> Edit(long id){
        string token = User.FindFirst("token")?.Value??"";
        string url = $"/api/v1/shipment/{id}";
        var response = await _gateway.GetAPI(url, token);
        var content = response.IsSuccessStatusCode ? await response.Content.ReadAsStringAsync() : string.Empty;
        return Ok(content);
    }

    [HttpPost("shipment")]
    public async Task<IActionResult> Add(ShipmentViewModel viewModel){
        try{
            string token = User.FindFirst("token")?.Value??"";
            string url = $"/api/v1/shipment";
            var result = await _gateway.PostAPI(viewModel, token, url);
            return Ok(result);
        }catch(Exception exception){
            throw new Exception(exception.Message);
        }
    }

    [HttpPut("shipment/{id}")]
    public async Task<IActionResult> Update(ShipmentViewModel viewModel){
        try{
            string token = User.FindFirst("token")?.Value??"";
            string url = $"/api/v1/shipment/{viewModel.Id}";
            var result = await _gateway.PutAPI(viewModel, token, url);
            return Ok(result);
        }catch(Exception exception){
            throw new Exception(exception.Message);
        }
    }

    [HttpPatch("shipment/{id}")]
    public async Task<IActionResult> StopService(long id){
        string token = User.FindFirst("token")?.Value??"";
        string url = $"/api/v1/shipment/{id}";
        var result = await _gateway.PatchAPI(new {}, token, url);
        return Ok(result);
    }

    [HttpDelete("shipment/{id}")]
    public async Task<IActionResult> Delete(long id){
        string token = User.FindFirst("token")?.Value??"";
        string url = $"/api/v1/shipment/{id}";
        var result = await _gateway.DeleteAPI(token, url);
        return Ok(result);
    }

    [HttpGet("shipment/check/{id}")]
    public async Task<IActionResult> CheckExistenceTransaction(long id){
        string token = User.FindFirst("token")?.Value??"";
        string url = $"/api/v1/shipment/check/{id}";
        var response = await _gateway.GetAPI(url, token);
        if(!response.IsSuccessStatusCode) return Conflict(await response.Content.ReadAsStringAsync());
        return Ok();
    }
}
