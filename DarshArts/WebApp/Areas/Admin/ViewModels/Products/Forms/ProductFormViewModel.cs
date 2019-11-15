using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Areas.Admin.ViewModels.Products.Forms
{
    public class ProductFormViewModel
    {
        public string Id { get; set; }
        
        [Required]
        [DisplayName("Product Name")]
        public string Name { get; set; }
        
        [Required]
        [DisplayName("Code Number")]
        public string LabelCode { get; set; }
        
        [Required]
        [DisplayName("Unit Price")]
        public decimal UnitPrice { get; set; }
        
        [Required]
        [DisplayName("Product Information")]
        public string Description { get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
    }
}
