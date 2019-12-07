using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Areas.Admin.ViewModels.Customers.Forms
{
    public class CustomerFormViewModel
    {
        public string Id { get; set; }
        
        [Required]
        [DisplayName("Customer Name")]
        public string Name { get; set; }

        [Required]
        [DisplayName("Mobile Number")]
        public string ContactNumber1 { get; set; }
                
        [DisplayName("Phone Number")]
        public string ContactNumber2 { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DisplayName("Address Line 1")]
        public string AddressLine1 { get; set; }

        
        [DisplayName("Address Line 2")]
        public string AddressLine2 { get; set; }

        [Required]
        public string Pincode { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string State { get; set; }

        [Required]
        public string Country { get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
    }
}
