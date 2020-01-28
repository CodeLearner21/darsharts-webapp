using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Areas.Admin.Dtos
{
    public class OrderDto
    {
        public Guid Id { get; set; }
        public string OrderCode { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }        
        public string Details { get; set; }
        public Guid CustomerId { get; set; }          
        public CustomerDetailsDto Customer { get; set; }        
        public OrderStatusTypeDto OrderStatusType { get; set; }
        public IEnumerable<OrderItemDto> OrderItems { get; set; }
        public decimal OrderTotalPrice { get; set; }

    }
}
