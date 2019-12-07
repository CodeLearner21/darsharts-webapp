using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Areas.Admin.Dtos;
using WebApp.Areas.Admin.ViewModels.Customers.Forms;

namespace WebApp.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerDetailsDto>> GetAllCustomersAsync();
        Task<CustomerDetailsDto> GetCustomerById(string id);
        Task<CustomerFormViewModel> GetCustomerForEditAsync(string id);
        Task<string> SaveCustomerAsync(CustomerFormViewModel customerForm);
        Task<bool> UpdateCustomerAsync(CustomerFormViewModel customerForm);
        Task<bool> DeleteCustomerAsync(string custId);
    }
}
