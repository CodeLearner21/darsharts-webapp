using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Areas.Admin.Dtos
{
    public class OrderItemDto
    {
        public string Id { get; set; }

        public string ProductId { get; set; }

        public string OrderId { get; set; }

        public int Quantity { get; set; }
        
        public string Details { get; set; }
    }
}
