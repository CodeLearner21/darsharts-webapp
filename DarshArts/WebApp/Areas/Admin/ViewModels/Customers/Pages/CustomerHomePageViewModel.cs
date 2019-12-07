using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Areas.Admin.Dtos;

namespace WebApp.Areas.Admin.ViewModels.Customers.Pages
{
    public class CustomerHomePageViewModel
    {
        public IEnumerable<CustomerDetailsDto> Customers { get; set; }
    }
}
