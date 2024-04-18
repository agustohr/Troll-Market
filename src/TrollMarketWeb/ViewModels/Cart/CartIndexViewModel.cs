namespace TrollMarketWeb.ViewModels;

public class CartIndexViewModel
{
    public List<CartViewModel> Carts { get; set; } = null!;
    public PaginationViewModel Pagination { get; set; } = null!;
}
