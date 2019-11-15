using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Areas.Admin.ViewModels.Products.Forms;
using WebApp.Entities;

namespace WebApp.Data
{
    public class DataProfile : Profile
    {
        public DataProfile()
        {
            CreateMap<ProductFormViewModel, Product>()
                .ReverseMap();
        }
    }
}
