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
    public class OrderStatusTypeRepository : IOrderStatusTypeRepository
    {
        private readonly ApplicationDbContext _context;
        private ILogger<OrderStatusTypeRepository> _logger;
        public OrderStatusTypeRepository(ApplicationDbContext context, ILogger<OrderStatusTypeRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<OrderStatusType>> GetAllAsync()
        {
            try
            {
                var statusTypes = await _context.OrderStatusTypes.ToListAsync();
                if (statusTypes != null)
                    return statusTypes;

                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("Exception occur in OrderStatusType Reposioty while getting all data: {0} at {1}", ex.Message, DateTime.UtcNow));
                throw new Exception(ex.Message);
            }
        }

        public async Task<OrderStatusType> GetAllAsync(string id)
        {
            try
            {
                var statusType = await _context.OrderStatusTypes.SingleOrDefaultAsync(st => st.Id == Guid.Parse(id));
                if (statusType != null)
                    return statusType;

                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("Exception occur in OrderStatusType Reposioty while getting single data: {0} at {1}", ex.Message, DateTime.UtcNow));
                throw new Exception(ex.Message);
            }
        }
    }
}
