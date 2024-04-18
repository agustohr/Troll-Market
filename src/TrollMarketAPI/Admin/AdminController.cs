using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TrollMarketAPI.Admin;

[Route("api/v1/[controller]")]
[ApiController]
[Authorize(Roles = "Admin")]
public class AdminController : ControllerBase
{
    private readonly AdminService _service;

    public AdminController(AdminService service)
    {
        _service = service;
    }

    [HttpPost]
    public IActionResult AddAdmin(NewAdminDto dto){
        try{
            _service.NewAdmin(dto);
            return Created("", $"Admin with username {dto.Username} was added successfully");
        }catch(Exception exception){
            return BadRequest($"Admin with username {dto.Username} failed to add");
        }
    }
}
