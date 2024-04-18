namespace TrollMarketAPI.History;

public class HistoryDto
{
    public string PurchaseDate { get; set; } = null!;
    public string SellerName { get; set; } = null!;
    public string BuyerName { get; set; } = null!;
    public string ProductName { get; set; } = null!;
    public int Quantity { get; set; }
    public string ShipmentName { get; set; } = null!;
    public string TotalPrice { get; set; } = null!;
}
