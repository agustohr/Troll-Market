namespace TrollMarketAPI.Shop;

public class ShopDetailDto
{
    public string Name { get; set; } = null!;
    public string Category { get; set; } = null!;
    public string? Description { get; set; }
    public string Price { get; set; } = null!;
    public string SellerName { get; set; } = null!;
}
