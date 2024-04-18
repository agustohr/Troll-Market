using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TrollMarketAPI.History;

[Route("api/v1/[controller]")]
[ApiController]
[Authorize(Roles = "Admin")]
public class HistoryController : ControllerBase
{
    private readonly HistoryService _service;

    public HistoryController(HistoryService service)
    {
        _service = service;
    }

    // [HttpGet("selectlist")]
    // public IActionResult GetSelectList(){
    //     var dtoSeller = _service.GetSelectListSeller();
    //     var dtoBuyer = _service.GetSelectListBuyer();
    //     return Ok(new { SelectListSeller = dtoSeller, SelectListBuyer = dtoBuyer });
    // }

    [HttpGet()]
    public IActionResult Get(int pageNumber = 1, string? sellerUsername = null, string? buyerUsername = null){
        var dto = _service.Get(pageNumber, sellerUsername, buyerUsername);
        return Ok(dto);
    }
}
