using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Areas.Admin.ViewModels.Stocks.Forms;
using WebApp.Areas.Admin.ViewModels.Stocks.Pages;
using WebApp.Services.Interfaces;

namespace WebApp.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class StocksController : Controller
    {
        private readonly IProductService _productService;
        private readonly IStockService _stockService;

        public StocksController(IProductService productService, IStockService stockService)
        {
            _productService = productService;
            _stockService = stockService;
        }
        public IActionResult Index()
        {
            var stocks = _stockService.GetAllAsync().Result;
            var viewModel = new StockMainPageViewModel { Stocks = stocks};
            return View(viewModel);
        }

        public IActionResult New()
        {

            var viewModel = new AddStockPageViewModel
            {
                StockForm = new StockFormViewModel(),
                Products = _productService.GetAllAsync().GetAwaiter().GetResult()
            };
            return View(viewModel);
        }

        public IActionResult Edit(string id)
        {
            var stock = _stockService.GetEditFormDataAsync(id).Result;
            var products = _productService.GetAllAsync().Result;

            var viewModel = new EditStockPageViewModel
            {
                StockForm = (stock != null) ? stock : new StockFormViewModel(),
                Products = products
            };
            return View(viewModel);
        }

        public IActionResult Save(StockFormViewModel stockForm)
        {
            if(stockForm.Id == null && ModelState.IsValid)
            {
                stockForm.DateCreated = DateTime.Now;
                stockForm.DateUpdated = DateTime.Now;
                var stockId = _stockService.SaveStockAsync(stockForm).Result;

                if(!string.IsNullOrWhiteSpace(stockId))
                {
                    TempData["NewStockId"] = stockId;
                    return RedirectToAction("Index", "Stocks");
                }
            }
            else if(stockForm.Id != null && ModelState.IsValid)
            {
                stockForm.DateUpdated = DateTime.Now;
                var result = _stockService.UpdateAsync(stockForm).Result;
                
                return RedirectToAction("Index", "Stocks");
            }
            return RedirectToAction("New", "Stocks");
        }

        [HttpPost]
        public IActionResult Delete(string stockId)
        {
            if (string.IsNullOrWhiteSpace(stockId))
                return BadRequest();

            var result = _stockService.DeleteAsync(stockId).Result;
            return RedirectToAction("Index", "Stocks");
        }
    }
}