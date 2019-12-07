using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Areas.Admin.Dtos;
using WebApp.Areas.Admin.ViewModels.Customers.Forms;
using WebApp.Areas.Admin.ViewModels.Orders.Forms;
using WebApp.Areas.Admin.ViewModels.Products.Forms;
using WebApp.Areas.Admin.ViewModels.Stocks.Forms;
using WebApp.Areas.Admin.ViewModels.Stocks.Pages;
using WebApp.Entities;

namespace WebApp.Data
{
    public class DataProfile : Profile
    {
        public DataProfile()
        {
            CreateMap<ProductFormViewModel, Product>()
                .ReverseMap();

            CreateMap<ProductDetailsDto, Product>()
                .ReverseMap();

            CreateMap<StockFormViewModel, Stock>()
                .ReverseMap();

            CreateMap<StockDetailsDto, Stock>()
                .ReverseMap();

            CreateMap<CustomerFormViewModel, Customer>()
                .ReverseMap();

            CreateMap<CustomerDetailsDto, Customer>()
                .ReverseMap();

            CreateMap<OrderFormViewModel, Order>()
                .ReverseMap();

            CreateMap<OrderStatusTypeDto, OrderStatusType>()
                .ReverseMap();

        }
    }
}
