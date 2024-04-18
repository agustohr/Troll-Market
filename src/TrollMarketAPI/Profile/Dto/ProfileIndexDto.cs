namespace TrollMarketAPI.Profile;

public class ProfileIndexDto
{
    public string Name { get; set; } = null!;
    public string Role { get; set; } = null!;
    public string Address { get; set; } = null!;
    public string Balance { get; set; } = null!;
    public List<HistoryUserDto> UserHistories { get; set; } = null!;
    public PaginationDto Pagination { get; set; } = null!;
}
