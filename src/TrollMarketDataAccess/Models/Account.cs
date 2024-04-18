using System;
using System.Collections.Generic;

namespace TrollMarketDataAccess.Models
{
    public partial class Account
    {
        public Account()
        {
            Products = new HashSet<Product>();
            Transactions = new HashSet<Transaction>();
        }

        public string Username { get; set; } = null!;
        public string? Password { get; set; }
        public string Role { get; set; } = null!;
        public string? Name { get; set; }
        public string? Address { get; set; }
        public decimal? Balance { get; set; }

        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
