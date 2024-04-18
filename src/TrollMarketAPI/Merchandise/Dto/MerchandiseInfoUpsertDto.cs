namespace TrollMarketAPI.Merchandise;

public class MerchandiseInfoUpsertDto
{
    public string Name { get; set; } = null!;
    public string Category { get; set; } = null!;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public string? PriceRp { get; set; }
    public bool IsDiscontinue { get; set; }
}
