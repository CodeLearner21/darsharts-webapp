using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Services.Interfaces;

namespace WebApp.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]    
    public class DashboardController : Controller
    {
        private readonly IOrderService _orderService;

        public DashboardController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetOrdersByTypeAjax(string orderType = null)
        {
            var orders = _orderService.GetAllOrders().Result;
            if (orders == null)
                return NotFound();
            if (orderType != null)
            {
                orders = orders.Where(o => o.OrderStatusType.Name == orderType).ToList();
                return Ok(orders);
            }

            return NotFound();
        }
    }
}