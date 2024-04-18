namespace TrollMarketWeb.ViewModels;

public class MerchandiseIndexViewModel
{
    public List<MerchandiseViewModel> Merchandises { get; set; } = null!;
    public PaginationViewModel Pagination { get; set; } = null!;
}
