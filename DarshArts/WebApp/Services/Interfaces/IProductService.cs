using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Areas.Admin.Dtos;
using WebApp.Areas.Admin.ViewModels.Products.Forms;

namespace WebApp.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDetailsDto>> GetAllAsync();
        Task<ProductDetailsDto> GetById(string id);
        Task<ProductFormViewModel> EditById(string id);
        Task<string> SaveAndGetIdAsync(ProductFormViewModel productForm);
        Task<bool> UpdateAsync(ProductFormViewModel productForm);
        Task<bool> DeleteAsync(string id);
    }
}
