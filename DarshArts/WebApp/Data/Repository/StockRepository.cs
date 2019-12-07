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
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<StockRepository> _logger;

        public StockRepository(ApplicationDbContext context, ILogger<StockRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<Stock>> GetAllAsync()
        {
            try
            {
                var stocks = await _context.Stocks.Include(st => st.Product).ToListAsync();
                if (stocks != null && stocks.Count > 0)
                    return stocks;

                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("Exception occur while getting stocks data: {0} at {1}", ex.Message, DateTime.UtcNow));
                throw new Exception("Cannot read stocks data: " + ex.Message);
            }
        }

        public async Task<Stock> GetOneAsync(string id)
        {
            try
            {
                var stock = await _context.Stocks.SingleOrDefaultAsync(st => st.Id == Guid.Parse(id));
                if (stock != null)
                    return stock;

                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("Exception occur while getting stock data for Id {0}: {1} at {2}", id, ex.Message, DateTime.UtcNow));
                throw new Exception(string.Format("Cannot read stocks data for Id {0}: {1}", id, ex.Message));
            }
        }

        public async Task<string> SaveAndReturnIdAsync(Stock stock)
        {
            try
            {
                await _context.Stocks.AddAsync(stock);
                await _context.SaveChangesAsync();

                return stock.Id.ToString();
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("Exception occur while saving data: {0} at {1}", ex.Message, DateTime.UtcNow));
                throw new Exception("Stock not saved in database: " + ex.Message);
            }
        }

        public async Task<bool> UpdateStock(Stock stock)
        {
            try
            {
                _context.Entry<Product>(stock.Product).State = EntityState.Unchanged;
                _context.Stocks.Update(stock);                
                return (await _context.SaveChangesAsync()) > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("Exception while updating stock: {0} at {1}", ex.Message, DateTime.UtcNow));
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteAsync(Stock stock)
        {
            try
            {
                _context.Entry(stock.Product).State = EntityState.Detached;
                _context.Stocks.Remove(stock);
                return (await _context.SaveChangesAsync()) > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("Exception while deleting stock: {0} at {1}", ex.Message, DateTime.UtcNow));
                throw new Exception(ex.Message);
            }
        }
    }
}
