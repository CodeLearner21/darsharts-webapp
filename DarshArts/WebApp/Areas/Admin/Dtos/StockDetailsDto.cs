using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Areas.Admin.Dtos
{
    public class StockDetailsDto
    {
        public string Id { get; set; }
        public int Quantity { get; set; }
        public Guid ProductId { get; set; }
        public ProductDetailsDto Product { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
    }
}
