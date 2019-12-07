using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp.Entities;

namespace WebApp.Data.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product> GetOneAsync(string id);
        Task<string> SaveAsync(Product product);
        Task<bool> UpdateAsync(Product product);
        Task<bool> DeleteAsync(string id);
    }
}
