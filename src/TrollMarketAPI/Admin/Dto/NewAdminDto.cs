using System.ComponentModel.DataAnnotations;

namespace TrollMarketAPI.Admin;

public class NewAdminDto
{
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;

    [Compare("Password", ErrorMessage = "Password Confirm doesn't match")]
    public string PasswordConfirm { get; set; } = null!;
}
