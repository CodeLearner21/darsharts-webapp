﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Areas.Admin.Dtos;
using WebApp.Areas.Admin.ViewModels.Orders.Forms;

namespace WebApp.Areas.Admin.ViewModels.Orders.Pages
{
    public class AddOrderPageViewModel
    {
        public OrderFormViewModel OrderForm { get; set; }        
        public IEnumerable<OrderStatusTypeDto> OrderStatusTypes { get; set; }
        public CustomerDetailsDto Customer { get; set; }
        public IEnumerable<ProductDetailsDto> Products { get; set; }
        public IEnumerable<StockDetailsDto> Stocks { get; set; }
        public CartItemListDisplayViewModel OrderItemList { get; set; }
    }
}
