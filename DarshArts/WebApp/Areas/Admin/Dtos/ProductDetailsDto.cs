using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Areas.Admin.Dtos
{
    public class ProductDetailsDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string LabelCode { get; set; }
        public decimal UnitPrice { get; set; }
        public string Description { get; set; }
    }
}
