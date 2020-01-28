using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Areas.Admin.Dtos
{
    public class CustomerDetailsDto
    {
        public string Id { get; set; }
        public string Name { get; set; }        
        public string ContactNumber1 { get; set; }
        public string ContactNumber2 { get; set; }
        public string Email { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string Pincode { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public IEnumerable<OrderDto> Orders { get; set; }
    }
}
