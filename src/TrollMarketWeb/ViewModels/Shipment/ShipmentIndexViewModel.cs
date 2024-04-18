namespace TrollMarketWeb.ViewModels;

public class ShipmentIndexViewModel
{
    public List<ShipmentViewModel> Shipments { get; set; } = null!;
    public PaginationViewModel Pagination { get; set; } = null!;
}
