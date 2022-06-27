using System;
using System.Collections.Generic;

namespace RetailApp.Models.DTO
{
    public class OrderDto
    {
        public OrderDto()
        {
            OrderItems = new List<OrderItemDto>();
        }
        public string OrderId { get; set; }
        public string OrderStatus { get; set; }
        public DateTime OrderDate { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string County { get; set; }
        public string Country { get; set; }
        public string EirCode { get; set; }
        public List<OrderItemDto> OrderItems { get; set; }

    }
}
