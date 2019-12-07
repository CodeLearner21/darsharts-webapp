using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Areas.Admin.Dtos;

namespace WebApp.Areas.Admin.ViewModels.Stocks.Forms
{
    public class StockFormViewModel
    {        
        public string Id { get; set; }
        
        [Required(ErrorMessage = "Please select product")]        
        public string ProductId { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]
        public int Quantity { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateUpdated { get; set; }

    }
}
