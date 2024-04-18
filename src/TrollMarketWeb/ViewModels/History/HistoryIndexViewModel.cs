using Microsoft.AspNetCore.Mvc.Rendering;

namespace TrollMarketWeb.ViewModels;

public class HistoryIndexViewModel
{
    public List<HistoryViewModel> Histories { get; set; } = null!;
    public PaginationViewModel Pagination { get; set; } = null!;

    public string? SellerUsername { get; set; }
    public string? BuyerUsername { get; set; }
    public List<SelectListItem>? Sellers { get; set; }
    public List<SelectListItem>? Buyers { get; set; }
}
