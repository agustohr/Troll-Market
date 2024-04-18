using System;
using System.Collections.Generic;

namespace TrollMarketDataAccess.Models
{
    public partial class Product
    {
        public Product()
        {
            Transactions = new HashSet<Transaction>();
        }

        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public string SellerUsername { get; set; } = null!;
        public string Category { get; set; } = null!;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public bool IsDiscontinue { get; set; }
        public bool IsDeleted { get; set; }

        public virtual Account SellerUsernameNavigation { get; set; } = null!;
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
