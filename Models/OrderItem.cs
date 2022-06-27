using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

#nullable disable

namespace RetailApp.Models
{
    public partial class OrderItem
    {
        [IgnoreDataMember]
        public int OrderItemId { get; set; }
        [IgnoreDataMember]
        public string OrderId { get; set; }
        public Guid? ProductId { get; set; }
        public int? Quantity { get; set; }
        [IgnoreDataMember]
        public decimal? Price { get; set; }
        [IgnoreDataMember]
        public DateTime? CreatedDate { get; set; }
        [IgnoreDataMember]
        public DateTime? UpdateDate { get; set; }
        [IgnoreDataMember]
        public virtual Order Order { get; set; }
        [IgnoreDataMember]
        public virtual Product Product { get; set; }
    }
}
