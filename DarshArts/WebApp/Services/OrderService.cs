using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Areas.Admin.Dtos;
using WebApp.Data.Interfaces;
using WebApp.Services.Interfaces;

namespace WebApp.Services
{
    public class OrderService : IOrderService
    {
        private IOrderStatusTypeRepository _orderStatusTypeRepository;
        private IMapper _mapper;
        private ILogger<OrderService> _logger;
        
        public OrderService(IOrderStatusTypeRepository orderStatusTypeRepository, IMapper mapper, ILogger<OrderService> logger)
        {
            _orderStatusTypeRepository = orderStatusTypeRepository;
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
    }
}
