using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Entities
{
    public class Order
    {
        public Guid Id { get; set; }
        public string OrderCode { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public string Details { get; set; }
        public Guid CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }
        public Guid OrderStatusTypeId { get; set; }
        public OrderStatusType OrderStatusType { get; set; }
        public IEnumerable<OrderItem> OrderItems { get; set; }
        public decimal OrderTotalPrice { get; set; }
    }
}
