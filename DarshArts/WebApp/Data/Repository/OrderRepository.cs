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
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<OrderRepository> _logger;

        public OrderRepository(ApplicationDbContext context, ILogger<OrderRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            try
            {
                var orders = await _context.Orders
                    .Include(o => o.OrderItems)
                    .Include(o => o.Customer)
                    .Include(o => o.OrderStatusType)
                    .OrderByDescending(o => o.DateCreated)
                    .ToListAsync();
                if (orders != null)
                    return orders;

                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("Exception occur in Order Repository while getting all orders: {0} at {1}", ex.Message, DateTime.UtcNow));
                throw new Exception(ex.Message);
            }
        }

        public async Task<Order> GetOneAsync(string id)
        {
            try
            {
                var order = await _context.Orders
                    .Include(o => o.OrderItems)
                    .ThenInclude(i => i.Product)
                    .Include(o => o.Customer)
                    .Include(o => o.OrderStatusType)
                    .SingleOrDefaultAsync(o => o.Id == Guid.Parse(id));
                if (order != null)
                    return order;

                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("Exception occur in Order Repository while getting single order: {0} at {1}", ex.Message, DateTime.UtcNow));
                throw new Exception(ex.Message);
            }
        }

        public async Task<string> SaveAsync(Order order)
        {
            try
            {
                string codeLable = "ORD";
                Random generator = new Random();
                string code = string.Format("{0}{1}", codeLable, generator.Next(0, 999999).ToString("D6"));
                do
                {
                    code = string.Format("{0}{1}", codeLable, generator.Next(0, 999999).ToString("D6"));
                    order.OrderCode = code;
                }
                while (_context.Orders.SingleOrDefault(o => o.OrderCode == code) != null);

                if (string.IsNullOrEmpty(order.OrderCode))
                    return null;

                _context.Orders.Add(order);
                await _context.SaveChangesAsync();

                return order.Id.ToString();
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("Exception occur in Order Repository: {0} at {1}", ex.Message, DateTime.UtcNow));
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> UpdateAsync(Order order)
        {
            try
            {                
                foreach(var item in order.OrderItems)
                {
                    _context.Entry(item).State = EntityState.Detached;
                }
                _context.Entry(order.OrderStatusType).State = EntityState.Detached;
                _context.Entry(order.Customer).State = EntityState.Detached;                
                _context.Orders.Update(order);
                return (await _context.SaveChangesAsync()) > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("Exception occur in Order Repository while updating order: {0} at {1}", ex.Message, DateTime.UtcNow));
                throw new Exception(ex.Message);
            }

            
        }

        public async Task<bool> DeleteAsync(string orderId)
        {            
            try
            {
                var order = _context.Orders.SingleOrDefault(o => o.Id == Guid.Parse(orderId));
                _context.Orders.Remove(order);

                return (await _context.SaveChangesAsync()) > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("Exception occur in Order Repository while deleting product: {0} at {1}", ex.Message, DateTime.UtcNow));
                throw new Exception(ex.Message);
            }
        }
    }
}
