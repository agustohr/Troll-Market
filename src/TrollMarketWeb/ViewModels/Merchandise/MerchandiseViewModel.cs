namespace TrollMarketWeb.ViewModels;

public class MerchandiseViewModel
{
    public long Id { get; set; }
    public string Name { get; set; } = null!;
    public string Category { get; set; } = null!;
    public string Discontinue { get; set; } = null!;
}
