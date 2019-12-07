using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Areas.Admin.Dtos;

namespace WebApp.Services.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderStatusTypeDto>> GetAllStatusTypes();
    }
}
