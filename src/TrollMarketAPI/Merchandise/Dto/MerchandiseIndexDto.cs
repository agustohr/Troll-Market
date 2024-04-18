namespace TrollMarketAPI.Merchandise;

public class MerchandiseIndexDto
{
    public List<MerchandiseDto> Merchandises { get; set; } = null!;
    public PaginationDto Pagination { get; set; } = null!;
}
