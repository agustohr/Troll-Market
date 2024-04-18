using System.ComponentModel.DataAnnotations;
using TrollMarketWeb.Validations;

namespace TrollMarketWeb.ViewModels;

public class AuthRegisterViewModel
{
    [Required(ErrorMessage = "Username must be filled")]
    [UniqueUsernameValidation]
    public string Username { get; set; } = null!;

    [Required(ErrorMessage = "Password must be filled")]
    public string Password { get; set; } = null!;

    [Required(ErrorMessage = "Password confirm must be filled")]
    [Compare("Password", ErrorMessage = "Password confirm doesn't match")]
    public string PasswordConfirm { get; set; } = null!;

    [Required(ErrorMessage = "Name must be filled")]
    public string Name { get; set; } = null!;

    [Required(ErrorMessage = "Address must be filled")]
    public string Address { get; set; } = null!;

    public string? Role { get; set; }
}
