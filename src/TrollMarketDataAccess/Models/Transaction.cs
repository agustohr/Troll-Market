using System;
using System.Collections.Generic;

namespace TrollMarketDataAccess.Models
{
    public partial class Transaction
    {
        public long Id { get; set; }
        public long ProductId { get; set; }
        public long ShipmentId { get; set; }
        public string BuyerUsername { get; set; } = null!;
        public DateTime? PurchaseDate { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }

        public virtual Account BuyerUsernameNavigation { get; set; } = null!;
        public virtual Product Product { get; set; } = null!;
        public virtual Shipment Shipment { get; set; } = null!;
    }
}
