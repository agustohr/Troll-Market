using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TrollMarketAPI.Shipments;

[Route("api/v1/[controller]")]
[ApiController]
[Authorize(Roles = "Admin")]
public class ShipmentController : ControllerBase
{
    private readonly ShipmentService _service;

    public ShipmentController(ShipmentService service)
    {
        _service = service;
    }

    [HttpGet()]
    public IActionResult Get(int pageNumber = 1){
        var dto = _service.Get(pageNumber);
        return Ok(dto);
    }

    [HttpGet("{id}")]
    public IActionResult Get(long id){
        var dto = _service.Get(id);
        return Ok(dto);
    }

    [HttpPost()]
    public IActionResult Add(ShipmentDto dto){
        try{
            _service.Insert(dto);
            return Created("", $"Shipment {dto.Name} wss added successfully");
        }catch(Exception exception){
            return Conflict($"Shipment {dto.Name} failed to add");
        }
    }

    [HttpPut("{id}")]
    public IActionResult Edit(long id, ShipmentDto dto){
        try{
            _service.Update(id, dto);
            return Ok("Shipment wss updated successfully");
        }catch(Exception exception){
            return Conflict($"Shipment {dto.Name} failed to update");
        }
        
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(long id){
        try{
            _service.SoftDelete(id);
            return Ok("Shipment was deleted successfully");
        }catch(Exception exception){
            return Conflict($"Shipment failed to delete");
        }
    }

    [HttpPatch("{id}")]
    public IActionResult StopService(long id){
        try{
            _service.StopService(id);
            return Ok("Shipment service was stopped successfully");
        }catch(Exception exception){
            return Conflict("Shipment failed to stop service");
        }
    }

    [HttpGet("check/{id}")]
    public IActionResult CheckTransaction(long id){
        var isExist = _service.CheckTransaction(id);
        if(isExist) return Conflict("Sorry, you cannot update or delete this shipment, because this shipment has already been transacted");
        return Ok();
    }
}
