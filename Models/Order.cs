using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

#nullable disable

namespace RetailApp.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderItems = new HashSet<OrderItem>();
        }
        [IgnoreDataMember]
        public string OrderId { get; set; }
        [IgnoreDataMember]
        public int OrderStatus { get; set; }
        [IgnoreDataMember]
        public DateTime OrderDate { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string County { get; set; }
        public string Country { get; set; }
        public string EirCode { get; set; }
        [IgnoreDataMember]
        public DateTime? UpdatedDate { get; set; }
        [IgnoreDataMember]
        public virtual OrderStatus OrderStatusNavigation { get; set; }        
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
