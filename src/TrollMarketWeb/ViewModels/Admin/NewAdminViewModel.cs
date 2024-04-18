using System.ComponentModel.DataAnnotations;
using TrollMarketWeb.Validations;

namespace TrollMarketWeb.ViewModels;

public class NewAdminViewModel
{
    [Required(ErrorMessage = "Username must be filled")]
    [UniqueUsernameValidation]
    public string Username { get; set; } = null!;

    [Required(ErrorMessage = "Password must be filled")]
    public string Password { get; set; } = null!;

    [Required(ErrorMessage = "Confirm Password must be filled")]
    [Compare("Password", ErrorMessage = "Password confirm doesn't match")]
    public string PasswordConfirm { get; set; } = null!;
}
