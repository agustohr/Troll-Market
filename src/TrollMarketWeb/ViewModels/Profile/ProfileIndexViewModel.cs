namespace TrollMarketWeb.ViewModels;

public class ProfileIndexViewModel
{
    public string Name { get; set; } = null!;
    public string Role { get; set; } = null!;
    public string Address { get; set; } = null!;
    public string Balance { get; set; } = null!;
    public List<HistoryUserViewModel> UserHistories { get; set; } = null!;
    public PaginationViewModel Pagination { get; set; } = null!;
}
