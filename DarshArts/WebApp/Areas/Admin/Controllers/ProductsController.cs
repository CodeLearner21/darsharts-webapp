using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApp.Areas.Admin.ViewModels.Products.Forms;
using WebApp.Areas.Admin.ViewModels.Products.Pages;
using WebApp.Models;
using WebApp.Services.Interfaces;

namespace WebApp.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]    
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(IProductService productService, ILogger<ProductsController> logger)
        {
            _productService = productService;
            _logger = logger;
        }
        public IActionResult Index()
        {
            var viewModel = new ProductPageViewModel
            {
                Products = _productService.GetAllAsync().Result
            };
            return View(viewModel);
        }

        public IActionResult New()
        {
            var viewModel = new AddPageViewModel 
            { 
                ProductForm = new ProductFormViewModel() 
            };
            ViewData["ReturnUrl"] = "Admin/Products";
            return View(viewModel);
        }

        public IActionResult Edit(string id)
        {
            var product = _productService.EditById(id).Result;
            var viewModel = new EditPageViewModel
            {
                ProductForm = product
            };
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Save(ProductFormViewModel productForm)
        {
            if(productForm.Id == null && ModelState.IsValid)
            {
                var productId = _productService.SaveAndGetIdAsync(productForm).GetAwaiter().GetResult();
                if(!string.IsNullOrEmpty(productId))
                {
                    return RedirectToAction("Index", "Products");
                }                
            }
            if (!string.IsNullOrWhiteSpace(productForm.Id) && ModelState.IsValid)
            {
                productForm.DateUpdated = DateTime.Now;
                var updateResult = _productService.UpdateAsync(productForm).Result;
                return RedirectToAction("Index", "Products");
            }
            return RedirectToAction("New", "Products");
        }

        [HttpPost]
        public IActionResult Delete(string productId)
        {
            try
            {
                var id = productId;
                var result = _productService.DeleteAsync(productId).Result;
                return RedirectToAction("Index", "Products");
            }
            catch (Exception ex)
            {
                _logger.LogError(string.Format("Exception occur in product controller: {0} at {1}", ex.Message, DateTime.UtcNow));
                return BadRequest();
            }
        }

    }
}