using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Areas.Admin.Dtos;

namespace WebApp.Areas.Admin.ViewModels.Stocks.Pages
{
    public class StockMainPageViewModel
    {
        public IEnumerable<StockDetailsDto> Stocks { get; set; }
    }
}
