namespace TrollMarketAPI.Authorizations;

public class AuthLoginDto
{
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Role { get; set; } = null!;
    // public string? Name { get; set; }
    // public string? Address { get; set; }
    // public decimal? Balanace { get; set; }
}
