using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Areas.Admin.Dtos;

namespace WebApp.Areas.Admin.ViewModels.Products.Pages
{
    public class ProductPageViewModel
    {
        public IEnumerable<ProductDetailsDto> Products { get; set; }
    }
}
