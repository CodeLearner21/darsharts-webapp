using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Areas.Admin.ViewModels.Products.Forms;

namespace WebApp.Services.Interfaces
{
    public interface IProductService
    {
        Task<string> SaveAndGetIdAsync(ProductFormViewModel productForm);
    }
}
