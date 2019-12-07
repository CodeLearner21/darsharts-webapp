using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Areas.Admin.Dtos;
using WebApp.Areas.Admin.ViewModels.Orders.Forms;
using WebApp.Areas.Admin.ViewModels.Orders.Pages;
using WebApp.Services.Interfaces;

namespace WebApp.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class OrdersController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly ICustomerService _customerService;
        private readonly IProductService _productService;
        public OrdersController(IOrderService orderService, ICustomerService customerService, IProductService productService)
        {
            _orderService = orderService;
            _customerService = customerService;
            _productService = productService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult New(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return BadRequest();

            var statusTypes = _orderService.GetAllStatusTypes().Result;
            var customer = _customerService.GetCustomerById(id).Result;
            var products = _productService.GetAllAsync().Result;

            var viewModel = new AddOrderPageViewModel
            {
                OrderForm = new OrderFormViewModel(),
                OrderStatusTypes = statusTypes,
                Products = products,
                Customer = customer,
                MaxOrderFieldsCount = products.Count()
            };
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Save(OrderFormViewModel orderForm)
        {
            var order = orderForm;
            return null;
        }

        [HttpPost]
        public IActionResult AddItemToList(string productId, int quantity, string details)
        {
            if (string.IsNullOrEmpty(productId) || quantity < 1)
                return BadRequest();

            return Ok(new OrderItemDto { ProductId = productId, Quantity = quantity, Details = details });
        }
    }
}