using System.ComponentModel.DataAnnotations;

namespace TrollMarketWeb.ViewModels;

public class ShopAddCartViewModel
{
    [Range(minimum:1, maximum:int.MaxValue, ErrorMessage = "Quantity cannot be 0")]
    public int Quantity { get; set; }

    [Range(minimum:1, maximum:int.MaxValue, ErrorMessage = "Shipment must be selected")]
    public long ShipmentId { get; set; }
}
