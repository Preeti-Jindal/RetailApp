using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RetailApp.Data;
using RetailApp.Models;
using RetailApp.Models.DTO;

namespace RetailApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly RetailAppContext _context;

        public OrdersController(RetailAppContext context)
        {
            _context = context;
        }

        // GET: api/Orders
        /// <summary>
        /// Retrieve a paginated list of orders
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrders()
        {
            var ordersContext = await _context.Orders.Include(i=>i.OrderItems).ToListAsync();
            var orders = new List<OrderDto>();

            foreach (var _order in ordersContext)
            {
                orders.Add(CreateOrderResponse(_order));
            }
            return orders;
        }

        // GET: api/Orders/5
        /// <summary>
        /// Retrieve a single order
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDto>> GetOrder(string id)
        {
            var order = await _context.Orders.Include(i => i.OrderItems).Where(x => x.OrderId == id).FirstOrDefaultAsync();

            if (order == null)
            {
                return NotFound();
            }

            return CreateOrderResponse(order);
        }

        // POST: api/Orders
        /// <summary>
        /// Create a new order
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<OrderDto>> PostOrder(Order order)
        {
            order.OrderId = Guid.NewGuid().ToString();
            order.OrderStatus = 1;
            foreach(var item in order.OrderItems)
            {
                item.Price = _context.Products.Where(p => p.ProductId == item.ProductId).First().Price * item.Quantity;
            }

            _context.Orders.Add(order);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (OrderExists(order.OrderId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreateOrderResponse(order);
        }

        // DELETE: api/Orders/5
        /// <summary>
        /// Cancel an order
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteOrder(string id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return true;
        }
        // PUT: api/UpdateOrderDeliveryAddress/
        /// <summary>
        /// Update the order delivery address
        /// </summary>
        [HttpPut]
        [Route("UpdateOrderDeliveryAddress")]
        public async Task<ActionResult<bool>> UpdateOrderDeliveryAddress(OrderDto order)
        {
            var _order = await _context.Orders.FindAsync(order.OrderId);
            if (order == null)
            {
                return NotFound();
            }

            _order.AddressLine1 = order.AddressLine1;
            _order.AddressLine2 = order.AddressLine2;
            _order.City = order.City;
            _order.County = order.County;
            _order.Country = order.Country;
            _order.EirCode = order.EirCode;

            await _context.SaveChangesAsync();

            return true;
        }
        // PUT: api/UpdateOrderItems/
        /// <summary>
        /// Update the order items
        /// </summary>
        [HttpPut]
        [Route("UpdateOrderItems")]
        public async Task<ActionResult<bool>> UpdateOrderItems(OrderDto order)
        {
            var orderItemsContext = _context.OrderItems.Where(x=>x.OrderId == order.OrderId);
            if (order == null)
            {
                return NotFound();
            }
            foreach (var orderItem in order.OrderItems)
            {
                var orderItemContext = new OrderItem();
                if (orderItem.OrderItemId != 0)//existing item
                {
                    orderItemContext = orderItemsContext.Where(i => i.OrderItemId == orderItem.OrderItemId).FirstOrDefault();
                }
                orderItemContext.ProductId = orderItem.ProductId;
                orderItemContext.Quantity = orderItem.Quantity;
                orderItemContext.Price = _context.Products.Where(p => p.ProductId == orderItem.ProductId).First().Price * orderItem.Quantity;

                if (orderItem.OrderItemId == 0)//new item
                {
                    orderItemContext.OrderId = order.OrderId;
                    _context.OrderItems.Add(orderItemContext);
                }
            }

            await _context.SaveChangesAsync();

            return true;
        }
        private void SetOrderItem(OrderItem orderItemContext)
        {
            
        }
        private bool OrderExists(string id)
        {
            return _context.Orders.Any(e => e.OrderId == id);
        }        

        private OrderDto CreateOrderResponse(Order order)
        {
            var _order = new OrderDto()
            {
                OrderId = order.OrderId,
                AddressLine1 = order.AddressLine1,
                AddressLine2 = order.AddressLine2,
                City = order.City,
                Country = order.Country,
                County = order.County,
                EirCode = order.EirCode,
                OrderStatus = _context.OrderStatuses.Where(os => os.OrderStatusId == order.OrderStatus).First().OrderStatusName               
            };
            foreach(var _orderItem in order.OrderItems)
            {
                _order.OrderItems.Add(new OrderItemDto()
                {
                    OrderId = _orderItem.OrderId,
                    OrderItemId = _orderItem.OrderItemId,
                    Price = _orderItem.Price,
                    Quantity = _orderItem.Quantity,
                    ProductId = _orderItem.ProductId
                });         
            }
            return _order;
        }
    }
}
