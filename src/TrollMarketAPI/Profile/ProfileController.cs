using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TrollMarketAPI.Profile;

[Route("api/v1/[controller]")]
[ApiController]
[Authorize(Roles = "Seller, Buyer")]
public class ProfileController : ControllerBase
{
    private readonly ProfileService _service;

    public ProfileController(ProfileService service)
    {
        _service = service;
    }

    [HttpGet()]
    public IActionResult Get(int pageNumber = 1){
        var username = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var dto = _service.Get(pageNumber, username);
        return Ok(dto);
    }

    [HttpPatch()]
    public IActionResult AddBalance(BalanceDto balance){
        var username = User.FindFirstValue(ClaimTypes.NameIdentifier);
        _service.AddBalance(username, balance);
        return Ok("Balance was added successfully");
    }
}
