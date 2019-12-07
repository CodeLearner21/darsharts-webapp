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
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<CustomerRepository> _Logger;
        public CustomerRepository(ApplicationDbContext context, ILogger<CustomerRepository> logger)
        {
            _context = context;
            _Logger = logger;
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            try
            {
                var customers = await _context.Customers.ToListAsync();
                if (customers.Count > 0)
                    return customers;

                return null;
            }
            catch (Exception ex)
            {
                _Logger.LogError(string.Format("Exception occur in customer repository while getting all members: {0} at {1}", ex.Message, DateTime.UtcNow));
                throw new Exception(ex.Message);
            }
        }

        public async Task<Customer> GetOneAsync(string id)
        {
            try
            {
                var customer = await _context.Customers.SingleOrDefaultAsync(c => c.Id == Guid.Parse(id));
                if (customer != null)
                    return customer;

                return null;
            }
            catch (Exception ex)
            {
                _Logger.LogError(string.Format("Exception occur in customer repository while getting single customer {0} at {1}", ex.Message, DateTime.UtcNow));
                throw new Exception(ex.Message);
            }
        }

        public async Task<string> SaveCustomer(Customer customer)
        {
            try
            {
                _context.Customers.Add(customer);
                await _context.SaveChangesAsync();
                return customer.Id.ToString();
            }
            catch (Exception ex)
            {
                _Logger.LogError(string.Format("Exception occur in customer repository while saving customer: {0} at {1}", ex.Message, DateTime.UtcNow));
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> UpdateAsync(Customer customer)
        {
            try
            {
                _context.Customers.Update(customer);
                return (await _context.SaveChangesAsync()) > 0;
            }
            catch (Exception ex)
            {
                _Logger.LogError(string.Format("Exception occur in customer repository while updating customer: {0} at {1}", ex.Message, DateTime.UtcNow));
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteCustomerAsync(Customer customer)
        {
            try
            {
                _context.Customers.Remove(customer);
                return (await _context.SaveChangesAsync()) > 0;
            }
            catch (Exception ex)
            {
                _Logger.LogError(string.Format("Exception occur in customer repository while deleting data: {0} at {1}", ex.Message, DateTime.UtcNow));
                throw new Exception(ex.Message);
            }
        }
    }
}
