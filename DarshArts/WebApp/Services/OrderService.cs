using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Areas.Admin.Dtos;
using WebApp.Areas.Admin.ViewModels.Orders.Forms;
using WebApp.Data.Interfaces;
using WebApp.Entities;
using WebApp.Services.Interfaces;

namespace WebApp.Services
{
    public class OrderService : IOrderService
    {        
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderStatusTypeRepository _orderStatusTypeRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<OrderService> _logger;
        
        public OrderService(IOrderRepository orderRepository, IOrderStatusTypeRepository orderStatusTypeRepository, ICustomerRepository customerRepository, IOrderItemRepository orderItemRepository, IMapper mapper, ILogger<OrderService> logger)
        {
            _orderRepository = orderRepository;
            _orderStatusTypeRepository = orderStatusTypeRepository;
            _customerRepository = customerRepository;
            _orderItemRepository = orderItemRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<OrderStatusTypeDto>> GetAllStatusTypes()
        {
            try
            {
                var statusTypes = await _orderStatusTypeRepository.GetAllAsync();
                if (statusTypes != null)
                    return _mapper.Map<IEnumerable<OrderStatusTypeDto>>(statusTypes);

                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("Exception occur in Order Service while getting all OrderStatusTypes: {0} at {1}", ex.Message, DateTime.UtcNow));
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<OrderDto>> GetAllOrders()
        {
            try
            {
                var orders = await _orderRepository.GetAllAsync();
                if (orders != null)
                    return _mapper.Map<IEnumerable<OrderDto>>(orders);

                return null;
            }
            catch (Exception ex)
            {

                _logger.LogError(string.Format("Exception occur in Order Service while getting all orders: {0} at {1}", ex.Message, DateTime.UtcNow));
                throw new Exception(ex.Message);
            }
        }

        public async Task<OrderDto> GetDetails(string id)
        {
            try
            {
                var order = await _orderRepository.GetOneAsync(id);
                if (order != null)
                    return _mapper.Map<OrderDto>(order);

                return null;
            }
            catch (Exception ex)
            {

                _logger.LogError(string.Format("Exception occur in Order Service while getting order details: {0} at {1}", ex.Message, DateTime.UtcNow));
                throw new Exception(ex.Message);
            }
        }

        public async Task<OrderFormViewModel> GetOrderToEdit(string id)
        {
            try
            {
                var order = await _orderRepository.GetOneAsync(id);
                if (order != null)
                    return _mapper.Map<OrderFormViewModel>(order);

                return null;
            }
            catch (Exception ex)
            {

                _logger.LogError(string.Format("Exception occur in Order Service while getting order details to edit: {0} at {1}", ex.Message, DateTime.UtcNow));
                throw new Exception(ex.Message);
            }
        }

        public async Task<string> SaveOrder(OrderFormViewModel orderForm)
        {
            try
            {                
                var order = _mapper.Map<Order>(orderForm);
                var result = await _orderRepository.SaveAsync(order);

                if (result != null)
                    return result;

                return null;
            }
            catch (Exception ex)
            {

                _logger.LogError(string.Format("Exception occur in Order Service while saving order: {0} at {1}", ex.Message, DateTime.UtcNow));
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> UpdateOrder(OrderFormViewModel orderForm)
        {
            try
            {
                
                var order = _mapper.Map<Order>(orderForm);
                order.Customer = await _customerRepository.GetOneAsync(orderForm.CustomerId);
                order.OrderItems = await _orderItemRepository.GetAllByOrderIdAsync(orderForm.Id);
                order.OrderStatusType = await _orderStatusTypeRepository.GetOneAsync(orderForm.OrderStatusTypeId);

                var result = await _orderRepository.UpdateAsync(order);
                return result;
            }
            catch (Exception ex)
            {

                _logger.LogError(string.Format("Exception occur in Order Service while updating order: {0} at {1}", ex.Message, DateTime.UtcNow));
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteOrder(string orderId)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(orderId))
                    return false;

                return await _orderRepository.DeleteAsync(orderId);
            }
            catch (Exception ex)
            {

                _logger.LogError(string.Format("Exception occur in Order Service while deleting order: {0} at {1}", ex.Message, DateTime.UtcNow));
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<OrderDto>> GetOrdersByStatus(string statusType)
        {
            var orders = await _orderRepository.GetAllAsync();
            if (orders == null)
                return null;

            var filteredOrders = orders.Where(o => o.OrderStatusType.Name == statusType).ToList();
            return _mapper.Map<IEnumerable<OrderDto>>(filteredOrders);
        }
    }
}
