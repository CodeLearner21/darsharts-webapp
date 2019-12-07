using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Areas.Admin.Dtos;
using WebApp.Areas.Admin.ViewModels.Stocks.Forms;

namespace WebApp.Areas.Admin.ViewModels.Stocks.Pages
{
    public class EditStockPageViewModel
    {
        public StockFormViewModel StockForm { get; set; }
        public IEnumerable<ProductDetailsDto> Products { get; set; }
    }
}
