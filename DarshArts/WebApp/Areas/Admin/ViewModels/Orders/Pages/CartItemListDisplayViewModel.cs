using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Areas.Admin.Dtos;

namespace WebApp.Areas.Admin.ViewModels.Orders.Pages
{
    public class CartItemListDisplayViewModel
    {
        public IEnumerable<CartItemDto> CartItems { get; set; }
    }
}
