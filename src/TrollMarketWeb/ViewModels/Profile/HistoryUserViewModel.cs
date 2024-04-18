namespace TrollMarketWeb.ViewModels;

public class HistoryUserViewModel
{
    public string PurchaseDate { get; set; } = null!;
    public string ProductName { get; set; } = null!;
    public int Quantity { get; set; }
    public string ShipmentName { get; set; } = null!;
    public string TotalPrice { get; set; } = null!;
}
