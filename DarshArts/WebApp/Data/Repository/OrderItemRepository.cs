using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Data.Interfaces;
using WebApp.Entities;

namespace WebApp.Data.Repository
{
    public class OrderItemRepository : IOrderItemRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<OrderItemRepository> _logger;
        public OrderItemRepository(ApplicationDbContext context, ILogger<OrderItemRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<OrderItem>> GetAllByOrderIdAsync(string orderId)
        {
            try
            {
                var orderItems = await _context.OrderItems.Where(i => i.OrderId == Guid.Parse(orderId)).ToListAsync();
                if (orderItems != null)
                    return orderItems;

                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception occur while getting order items of order number {0}: {1} at {2}", orderId, ex.Message, DateTime.UtcNow);
                throw new Exception(ex.Message);
            }
        }
    }
}
