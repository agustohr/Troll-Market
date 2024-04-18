using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TrollMarketAPI.Cart;

[Route("api/v1/[controller]")]
[ApiController]
[Authorize(Roles = "Buyer")]
public class CartController : ControllerBase
{
    private readonly CartService _service;

    public CartController(CartService service)
    {
        _service = service;
    }

    [HttpGet()]
    public IActionResult Get(int pageNumber = 1){
        var username = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var dto = _service.Get(pageNumber, username);
        return Ok(dto);
    }

    [HttpPut("purchase")]
    public IActionResult Purchase(){
        try{
            var username = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = _service.PurchaseAll(username);
            return Ok(result);
        }catch(Exception exception){
            return Conflict(exception.Message);
        }
    }

    [HttpDelete("{id}")]
    public IActionResult Remove(long id){
        _service.Remove(id);
        return Ok("Cart was removed successfully");
    }
}
