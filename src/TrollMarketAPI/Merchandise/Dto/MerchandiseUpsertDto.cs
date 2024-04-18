namespace TrollMarketAPI.Merchandise;

public class MerchandiseUpsertDto
{
    // public long Id { get; set; }
    public string Name { get; set; } = null!;
    public string? SellerUsername { get; set; }
    public string Category { get; set; } = null!;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public bool IsDiscontinue { get; set; }
}
