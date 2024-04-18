using Microsoft.AspNetCore.Mvc.Rendering;

namespace TrollMarketAPI.History;

public class HistoryIndexDto
{
    public List<HistoryDto> Histories { get; set; } = null!;
    public PaginationDto Pagination { get; set; } = null!;
    public string? Seller { get; set; }
    public string? Buyer { get; set; }
    public List<SelectListItem>? Sellers { get; set; }
    public List<SelectListItem>? Buyers { get; set; }
}
