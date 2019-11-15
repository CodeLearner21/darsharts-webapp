using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string LabelCode { get; set; }
        public decimal UnitPrice { get; set; }
        public string Description { get; set; }
    }
}
