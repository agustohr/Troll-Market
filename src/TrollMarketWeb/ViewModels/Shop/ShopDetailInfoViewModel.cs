namespace TrollMarketWeb.ViewModels;

public class ShopDetailInfoViewModel
{
    public string Name { get; set; } = null!;
    public string Category { get; set; } = null!;
    public string? Description { get; set; }
    public string Price { get; set; } = null!;
    public string SellerName { get; set; } = null!;
}
