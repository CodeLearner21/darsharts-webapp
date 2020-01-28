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
        public string Id { get; set; }
        public string OrderCode { get; set; }
        [Required]
        public string OrderStatusTypeId { get; set; }        
        [Required]
        public string CustomerId { get; set; }
        public string Details { get; set; }
        public IEnumerable<OrderItemDto> OrderItems { get; set; }
        public decimal OrderTotalPrice { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }

    }
}
