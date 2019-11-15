using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Areas.Admin.ViewModels.Products.Forms;
using WebApp.Areas.Admin.ViewModels.Products.Pages;
using WebApp.Services.Interfaces;

namespace WebApp.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]    
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }
        public IActionResult Index()
        {

            return View();
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
            return RedirectToAction("New", "Products");
        }
    }
}