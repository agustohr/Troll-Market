namespace TrollMarketWeb.ViewModels;

public class CartViewModel
{
    public long Id { get; set; }
    public string ProductName { get; set; } = null!;
    public int Quantity { get; set; }
    public string ShipmentName { get; set; } = null!;
    public string SellerName { get; set; } = null!;
    public string TotalPrice { get; set; } = null!;
}
