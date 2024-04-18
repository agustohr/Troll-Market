namespace TrollMarketAPI.Cart;

public class CartIndexDto
{
    public List<CartDto> Carts { get; set; } = null!;
    public PaginationDto Pagination { get; set; } = null!;
}
