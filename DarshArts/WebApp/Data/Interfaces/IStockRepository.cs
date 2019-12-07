using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Entities;

namespace WebApp.Data.Interfaces
{
    public interface IStockRepository
    {
        Task<IEnumerable<Stock>> GetAllAsync();
        Task<Stock> GetOneAsync(string id);
        Task<string> SaveAndReturnIdAsync(Stock stock);
        Task<bool> UpdateStock(Stock stock);
        Task<bool> DeleteAsync(Stock stock);
    }
}
