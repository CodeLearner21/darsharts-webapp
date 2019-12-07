using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Areas.Admin.Dtos;
using WebApp.Areas.Admin.ViewModels.Customers.Forms;
using WebApp.Data.Interfaces;
using WebApp.Entities;
using WebApp.Services.Interfaces;

namespace WebApp.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CustomerService> _logger;

        public CustomerService(ICustomerRepository customerRepository, IMapper mapper, ILogger<CustomerService> logger)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<CustomerDetailsDto>> GetAllCustomersAsync()
        {
            try
            {                
                var customers = await _customerRepository.GetAllAsync();
                if (customers != null)
                    return _mapper.Map<IEnumerable<CustomerDetailsDto>>(customers);

                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("Exception occur in customer service while getting all data: {0} at {1}", ex.Message, DateTime.Now));
                throw new Exception(ex.Message);
            }
        }

        public async Task<CustomerDetailsDto> GetCustomerById(string id)
        {
            try
            {
                var customer = await _customerRepository.GetOneAsync(id);
                if (customer != null)
                    return _mapper.Map<CustomerDetailsDto>(customer);

                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("Exception occur in customer service while geting single customer record: {0} at {1}", ex.Message, DateTime.UtcNow));
                throw new Exception(ex.Message);
            }
        }

        public async Task<CustomerFormViewModel> GetCustomerForEditAsync(string id)
        {
            try
            {
                var customer = await _customerRepository.GetOneAsync(id);
                if (customer != null)
                    return _mapper.Map<CustomerFormViewModel>(customer);

                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("Exception occur in customer service while geting single customer record: {0} at {1}", ex.Message, DateTime.UtcNow));
                throw new Exception(ex.Message);
            }
        }

        public async Task<string> SaveCustomerAsync(CustomerFormViewModel customerForm)
        {
            try
            {
                customerForm.DateCreated = DateTime.Now;
                customerForm.DateUpdated = DateTime.Now;

                var customer = _mapper.Map<Customer>(customerForm);
                var result = await _customerRepository.SaveCustomer(customer);
                if (result != null)
                    return result;

                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("Exception occur in customer service while saving data: {0} at {1}", ex.Message, DateTime.Now));
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> UpdateCustomerAsync(CustomerFormViewModel customerForm)
        {
            try
            {                
                customerForm.DateUpdated = DateTime.Now;

                var customer = _mapper.Map<Customer>(customerForm);
                var result = await _customerRepository.UpdateAsync(customer);
                
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("Exception occur in customer service while updating data: {0} at {1}", ex.Message, DateTime.Now));
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteCustomerAsync(string custId)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(custId))
                    return false;

                var customer = await _customerRepository.GetOneAsync(custId);
                if(customer == null)
                    return false;

                var result = await _customerRepository.DeleteCustomerAsync(customer);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("Exception occur in customer service while deleting data: {0} at {1}", ex.Message, DateTime.Now));
                throw new Exception(ex.Message);
            }
        }
    }
}
