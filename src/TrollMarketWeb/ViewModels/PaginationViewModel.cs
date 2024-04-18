namespace TrollMarketWeb.ViewModels;

public class PaginationViewModel
{
    public int TotalItems { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalPages {
        get{
            return (int)Math.Ceiling((double)TotalItems / PageSize);
        } 
    }
}
