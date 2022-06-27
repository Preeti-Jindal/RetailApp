using System;
using System.Collections.Generic;

#nullable disable

namespace RetailApp.Models
{
    public partial class Product
    {
        public Product()
        {
            OrderItems = new HashSet<OrderItem>();
        }

        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal? Price { get; set; }
        public int? Quantity { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
