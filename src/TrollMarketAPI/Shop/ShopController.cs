using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TrollMarketAPI.Shop;

[Route("api/v1/[controller]")]
[ApiController]
[Authorize(Roles = "Buyer")]
public class ShopController : ControllerBase
{
    private readonly ShopService _service;

    public ShopController(ShopService service)
    {
        _service = service;
    }

    [HttpGet()]
    public IActionResult Get(int pageNumber = 1, string? productName = "", string? category = "", string? description = ""){
        var dto = _service.Get(pageNumber, productName, category, description);
        return Ok(dto);
    }

    [HttpGet("detail/{id}")]
    public IActionResult Get(long id){
        var dto = _service.Get(id);
        return Ok(dto);
    }

    [HttpPost("addcart/{id}")]
    public IActionResult AddToCart(long id, ShopAddCartDto dto){
        try{
            var buyerUsername = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _service.AddToCart(id, buyerUsername, dto);
            return Ok("Product was added to cart successfully");
        }catch(Exception exception){
            return Conflict("Product failed to add cart");
        }
    }

    [HttpGet("selectlistshipments")]
    public IActionResult GetSelectListShipment(){
        var dto = _service.GetSelectListShipment();
        return Ok(dto);
    }
}
