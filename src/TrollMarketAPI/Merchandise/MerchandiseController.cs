using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TrollMarketAPI.Merchandise;

[Route("api/v1/[controller]")]
[ApiController]
[Authorize(Roles = "Seller")]
public class MerchandiseController : ControllerBase
{
    private readonly MerchandiseService _service;

    public MerchandiseController(MerchandiseService service)
    {
        _service = service;
    }

    [HttpGet()]
    public IActionResult Get(int pageNumber = 1){
        var username = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var dto = _service.Get(pageNumber, username);
        return Ok(dto);
    }

    [HttpPost()]
    public IActionResult Add(MerchandiseInfoUpsertDto dto){
        try{
            var username = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _service.Insert(username, dto);
            return Created("", $"Product {dto.Name} was added successfully");
        }catch(Exception exception){
            return Conflict($"Product {dto.Name} failed to add");
        }
    }

    [HttpPut("{id}")]
    public IActionResult Edit(long id, MerchandiseUpsertDto dto){
        try{
            _service.Update(id, dto);
            return Ok($"Product {dto.Name} failed to add");
        }catch(Exception exception){
            return Conflict($"Product {dto.Name} failed to update");
        }
    }

    [HttpGet("{id}")]
    public IActionResult GetByID(long id){
        var dto = _service.Get(id);
        return Ok(dto);
    }

    [HttpPatch("{id}")]
    public IActionResult SetDiscontinue(long id){
        try{
            _service.SetDiscontinue(id);
            return Ok("Product has been set to discontinue");
        }catch(Exception exception){
            return Conflict("Product status failed to set discontinue");
        }
    }

    [HttpGet("check/{id}")]
    public IActionResult CheckExistenceDependency(long id){
        var isExist = _service.CheckExistenceDependency(id);
        if(isExist) return Conflict("Sorry, you cannot update or delete this product, because this product has already been transacted");
        return Ok();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(long id){
        try{
            _service.SoftDelete(id);
            return Ok("Product was deleted successfully");
        }catch(Exception exception){
            return Conflict("Product failed to delete");
        }
    }
}
