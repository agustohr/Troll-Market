using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TrollMarketWeb.Controllers;

[Authorize]
public class HomeController : Controller
{
    [HttpGet("home")]
    public IActionResult Index(){
        return View();
    }
}
