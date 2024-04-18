using Microsoft.AspNetCore.Mvc;

namespace TrollMarketAPI.Authorizations;

[Route("api/v1/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly AuthService _service;

    public AuthController(AuthService service)
    {
        _service = service;
    }

    [HttpPost("login")]
    public IActionResult Login(AuthLoginDto request){
        try{
            var response = _service.GetToken(request);
            return Ok(response.Token);
        }catch(Exception exception){
            return Unauthorized(exception.Message);
        }
    }

    [HttpPost("register")]
    public IActionResult Register(AuthRegisterDto viewModel){
        _service.AddNewRegister(viewModel);
        return Ok("Register Success");
    }

    // [HttpGet()]
}
