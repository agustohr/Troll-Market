using System;
using System.Collections.Generic;

namespace TrollMarketDataAccess.Models
{
    public partial class Shipment
    {
        public Shipment()
        {
            Transactions = new HashSet<Transaction>();
        }

        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public bool IsService { get; set; }
        public bool IsDeleted { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
