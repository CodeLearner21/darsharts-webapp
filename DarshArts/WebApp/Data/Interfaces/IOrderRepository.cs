using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Entities;

namespace WebApp.Data.Interfaces
{
    public interface IOrderRepository
    {
        Task<string> SaveAsync(Order order);
        Task<Order> GetOneAsync(string id);
        Task<IEnumerable<Order>> GetAllAsync();
        Task<bool> UpdateAsync(Order order);
        Task<bool> DeleteAsync(string orderId);
    }
}
