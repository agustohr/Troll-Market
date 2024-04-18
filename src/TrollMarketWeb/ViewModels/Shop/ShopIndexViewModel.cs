namespace TrollMarketWeb.ViewModels;

public class ShopIndexViewModel
{
    public List<ShopViewModel> ShopItems { get; set; } = null!;
    public PaginationViewModel Pagination { get; set; } = null!;
}
