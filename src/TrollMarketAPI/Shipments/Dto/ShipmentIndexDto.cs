namespace TrollMarketAPI.Shipments;

public class ShipmentIndexDto
{
    public List<ShipmentDto> Shipments { get; set; } = null!;
    public PaginationDto Pagination { get; set; } = null!;
}
