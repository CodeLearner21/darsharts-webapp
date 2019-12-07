using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Areas.Admin.Dtos;

namespace WebApp.Areas.Admin.ViewModels.Orders.Forms
{
    public class OrderFormViewModel
    {
        public string OrderId { get; set; }
        
        [Required]
        public string OrderStatusTypeId { get; set; }
        
        [Required]
        public string CustomerId { get; set; }        

        public string Details { get; set; }

        public IEnumerable<OrderItemDto> OrderItems { get; set; }

    }
}
