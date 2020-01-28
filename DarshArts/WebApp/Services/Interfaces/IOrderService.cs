using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Areas.Admin.Dtos;
using WebApp.Areas.Admin.ViewModels.Orders.Forms;

namespace WebApp.Services.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderStatusTypeDto>> GetAllStatusTypes();
        Task<IEnumerable<OrderDto>> GetAllOrders();
        Task<OrderDto> GetDetails(string id);
        Task<OrderFormViewModel> GetOrderToEdit(string id);
        Task<string> SaveOrder(OrderFormViewModel orderForm);
        Task<bool> UpdateOrder(OrderFormViewModel orderForm);
        Task<bool> DeleteOrder(string orderId);
        Task<IEnumerable<OrderDto>> GetOrdersByStatus(string statusType);
    }
}
