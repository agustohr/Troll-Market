using System.ComponentModel.DataAnnotations;

namespace TrollMarketWeb.ViewModels;

public class AuthLoginViewModel
{
    [Required(ErrorMessage = "Username must be filled")]
    public string Username { get; set; } = null!;

    [Required(ErrorMessage = "Password must be filled")]
    public string Password { get; set; } = null!;

    [Required(ErrorMessage = "Role must be choose")]
    public string Role { get; set; } = null!;
}
