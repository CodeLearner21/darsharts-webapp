using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Entities
{
    public class Invoice
    {
        public Guid Id { get; set; }

        public Guid OrderId { get; set; }
        [ForeignKey("OrderId")]
        public Order Order { get; set; }

        public Guid CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }

        public decimal DiscountAmount { get; set; }
        
        public decimal TaxAmount { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
