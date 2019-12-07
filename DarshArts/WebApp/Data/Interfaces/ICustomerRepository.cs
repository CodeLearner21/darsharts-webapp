using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Entities;

namespace WebApp.Data.Interfaces
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> GetAllAsync();
        Task<Customer> GetOneAsync(string id);
        Task<string> SaveCustomer(Customer customer);
        Task<bool> UpdateAsync(Customer customer);
        Task<bool> DeleteCustomerAsync(Customer customer);
    }
}
