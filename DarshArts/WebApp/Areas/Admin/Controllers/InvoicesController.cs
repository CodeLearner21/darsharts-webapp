using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Areas.Admin.ViewModels.Invoices.Pages;
using WebApp.Services.Interfaces;

namespace WebApp.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class InvoicesController : Controller
    {
        private readonly IOrderService _orderService;


        public InvoicesController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult New(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return BadRequest();

            var order = _orderService.GetDetails(id).Result;

            var viewModel = new NewInvoicePageViewModel { Order = order };

            return View(viewModel);
        }
    }
}