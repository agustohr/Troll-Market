namespace TrollMarketAPI.Shipments;

public class ShipmentDto
{
    public long Id { get; set; }
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }
    public string? PriceRp { get; set; }
    public bool IsService { get; set; }
}
