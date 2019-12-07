using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Areas.Admin.ViewModels.Customers.Forms;
using WebApp.Areas.Admin.ViewModels.Customers.Pages;
using WebApp.Services.Interfaces;

namespace WebApp.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]    
    public class CustomersController : Controller
    {
        private readonly ICustomerService _customerService;
        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }
        public IActionResult Index()
        {
            var customers = _customerService.GetAllCustomersAsync().Result;

            var viewModel = new CustomerHomePageViewModel { Customers = customers };

            return View(viewModel);
        }

        public IActionResult Details(string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest();

            var customer = _customerService.GetCustomerById(id).Result;
            if (customer == null)
                BadRequest();

            var viewModel = new DetailsPageViewModel { Customer = customer };

            return View(viewModel);
        }

        public IActionResult New()
        {
            var viewModel = new AddCustomerPageViewModel
            {
                CustomerForm = new CustomerFormViewModel()
            };
            return View(viewModel);
        }

        public IActionResult Edit(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return BadRequest();

            var customer = _customerService.GetCustomerForEditAsync(id).Result;
            if (customer == null)
                return BadRequest();

            var viewModel = new EditCustomerPageViewModel { CustomerForm = customer };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Save(CustomerFormViewModel customerForm)
        {
            if(customerForm.Id == null && ModelState.IsValid)
            {
                var result = _customerService.SaveCustomerAsync(customerForm).Result;
                if(!string.IsNullOrWhiteSpace(result))
                {
                    TempData["NewCustomerId"] = result;
                    return RedirectToAction("Index", "Customers");
                }

            }
            else if(!string.IsNullOrEmpty(customerForm.Id) && ModelState.IsValid)
            {
                var result = _customerService.UpdateCustomerAsync(customerForm).Result;
                if (result)
                    return RedirectToAction("Index", "Customers");

                return BadRequest();
            }

            return BadRequest();
        }

        [HttpPost]
        public IActionResult Delete(string custId)
        {
            if (string.IsNullOrWhiteSpace(custId))
                return BadRequest();
            var result = _customerService.DeleteCustomerAsync(custId).Result;
            if (result)
            {
                return RedirectToAction("Index", "Customers");
            }
            return BadRequest();
        }

        [HttpGet("FindByName")]
        public IActionResult FindByName(string term)
        {
            if (string.IsNullOrWhiteSpace(term))
                return BadRequest();
            var customers = _customerService.GetAllCustomersAsync().Result;

            return null;
        }
        
    }
}