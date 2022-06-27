using System;

namespace RetailApp.Models.DTO
{
    public class OrderItemDto
    {
        public int OrderItemId { get; set; }
        public string OrderId { get; set; }
        public Guid? ProductId { get; set; }
        public int? Quantity { get; set; }
        public decimal? Price { get; set; }
    }
}
