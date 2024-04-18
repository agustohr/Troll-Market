using System.ComponentModel.DataAnnotations;

namespace TrollMarketWeb.ViewModels;

public class MerchandiseInfoUpsertViewModel
{
    // public long Id { get; set; }
    [Required(ErrorMessage = "Product must be filled")]
    public string Name { get; set; } = null!;
    // public string? SellerUsername { get; set; }

    [Required(ErrorMessage = "Product must be filled")]
    public string Category { get; set; } = null!;
    public string? Description { get; set; }

    [Range(minimum:0.01, maximum:double.MaxValue, ErrorMessage = "Price cannot be 0")]
    public decimal Price { get; set; }
    public string? PriceRp { get; set; }
    public bool IsDiscontinue { get; set; }
}
