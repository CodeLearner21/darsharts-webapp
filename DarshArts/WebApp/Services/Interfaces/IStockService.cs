using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Areas.Admin.Dtos;
using WebApp.Areas.Admin.ViewModels.Stocks.Forms;
using WebApp.Areas.Admin.ViewModels.Stocks.Pages;

namespace WebApp.Services.Interfaces
{
    public interface IStockService
    {
        Task<IEnumerable<StockDetailsDto>> GetAllAsync();
        Task<StockFormViewModel> GetEditFormDataAsync(string id);
        Task<string> SaveStockAsync(StockFormViewModel stockForm);
        Task<bool> UpdateAsync(StockFormViewModel stockForm);
        Task<bool> DeleteAsync(string stockId);
    }
}
