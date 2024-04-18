using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrollMarketWeb.APIGateway;
using TrollMarketWeb.ViewModels;

namespace TrollMarketWeb.Controllers;

// [ApiController]
[Authorize(Roles = "Seller")]
public class MerchandiseController : Controller
{
    private readonly ConfigureAPI _gateway;

    public MerchandiseController(ConfigureAPI configureAPI)
    {
        _gateway = configureAPI;
    }

    [HttpGet("merchandise")]
    public async Task<IActionResult> Index(int pageNumber = 1, string? resultCheck = null){
        ViewBag.Message = resultCheck;
        string token = User.FindFirst("token")?.Value??"";
        string url = $"/api/v1/merchandise?pageNumber={pageNumber}";
        var response = await _gateway.GetAPI(url, token);
        var viewModel = response.IsSuccessStatusCode ? await response.Content.ReadFromJsonAsync<MerchandiseIndexViewModel>() : null;
        return View(viewModel);
    }

    [HttpGet("merchandise/add")]
    public IActionResult Add(){
        ViewBag.Action = "Add";
        return View("Upsert", new MerchandiseInfoUpsertViewModel());
    }

    private async Task<string> CheckExistenceDependency(long id, string token){
        var url = $"/api/v1/merchandise/check/{id}";
        var response = await _gateway.GetAPI(url, token);
        if(!response.IsSuccessStatusCode) return await response.Content.ReadAsStringAsync();
        return string.Empty;
    }

    [HttpGet("merchandise/edit/{id}")]
    public async Task<IActionResult> Edit(long id){
        var token = User.FindFirst("token")?.Value??"";
        var resultCheck = await CheckExistenceDependency(id, token);
        if(resultCheck != string.Empty){
            return RedirectToAction("Index", new { resultCheck });
        }else{
            var url = $"/api/v1/merchandise/{id}";
            var response = await _gateway.GetAPI(url, token);
            var viewModel = response.IsSuccessStatusCode ? await response.Content.ReadFromJsonAsync<MerchandiseInfoUpsertViewModel>() : null;
            ViewBag.Action = "Edit";
            return View("Upsert", viewModel);
        }
    }

    [HttpGet("merchandise/info/{id}")]
    public async Task<IActionResult> Info(long id){
        var token = User.FindFirst("token")?.Value??"";
        var url = $"/api/v1/merchandise/{id}";
        var response = await _gateway.GetAPI(url, token);
        var viewModel = response.IsSuccessStatusCode ? await response.Content.ReadFromJsonAsync<MerchandiseInfoUpsertViewModel>() : null;
        return Ok(viewModel);
    }

    [HttpPost("merchandise/add")]
    public async Task<IActionResult> Insert(MerchandiseInfoUpsertViewModel viewModel){
        if(ModelState.IsValid){
            try{
                var token = User.FindFirst("token")?.Value??"";
                string url = "/api/v1/merchandise";
                var result = await _gateway.PostAPI(viewModel, token, url);
                ViewBag.Message = result;
                return View("Upsert", viewModel);
            }catch(Exception exception){
                ViewBag.Message = exception.Message;
                return View("Upsert", viewModel);
            }
        }
        return View("Upsert", viewModel);
    }

    [HttpPost("merchandise/edit/{id}")]
    public async Task<IActionResult> Update(long id, MerchandiseInfoUpsertViewModel viewModel){
        if(ModelState.IsValid){
            try{
                string token = User.FindFirst("token")?.Value??"";
                string url = $"/api/v1/merchandise/{id}";
                var result = await _gateway.PutAPI(viewModel, token, url);
                ViewBag.Message = result;
                return View("Upsert", viewModel);
            }catch(Exception exception){
                ViewBag.Message = exception.Message;
                return View("Upsert", viewModel);
            }
        }
        return View("Upsert", viewModel);
    }

    [HttpPatch("merchandise/discontinue/{id}")]
    public async Task<IActionResult> SetDiscontinue(long id){
        string token = User.FindFirst("token")?.Value??"";
        string url = $"/api/v1/merchandise/{id}";
        var result = await _gateway.PatchAPI(new {}, token, url);
        return Ok(result);
    }

    [HttpDelete("merchandise/{id}")]
    public async Task<IActionResult> Delete(long id){
        string token = User.FindFirst("token")?.Value??"";
        string url = $"/api/v1/merchandise/{id}";
        var resultCheck = await CheckExistenceDependency(id, token);
        if(resultCheck != string.Empty){
            return Conflict(resultCheck);
        }else{
            var result = await _gateway.DeleteAPI(token, url);
            return Ok(result);
        }
    }
}
