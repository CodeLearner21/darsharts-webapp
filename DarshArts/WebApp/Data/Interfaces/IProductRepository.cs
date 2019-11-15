using System.Threading.Tasks;
using WebApp.Entities;

namespace WebApp.Data.Interfaces
{
    public interface IProductRepository
    {
        Task<string> SaveAsync(Product product);
    }
}
