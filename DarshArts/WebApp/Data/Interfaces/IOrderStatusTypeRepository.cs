using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Entities;

namespace WebApp.Data.Interfaces
{
    public interface IOrderStatusTypeRepository
    {
        Task<IEnumerable<OrderStatusType>> GetAllAsync();
        Task<OrderStatusType> GetAllAsync(string id);
    }
}
