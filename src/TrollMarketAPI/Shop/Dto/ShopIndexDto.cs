namespace TrollMarketAPI.Shop;

public class ShopIndexDto
{
    public List<ShopDto> ShopItems { get; set; } = null!;
    public PaginationDto Pagination { get; set; } = null!;
}
