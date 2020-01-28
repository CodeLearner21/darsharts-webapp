using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApp.Areas.Admin.Dtos;
using WebApp.Areas.Admin.ViewModels.Orders.Forms;
using WebApp.Areas.Admin.ViewModels.Orders.Pages;
using WebApp.Services.Interfaces;
using WebApp.Extensions;
using System.Linq;

namespace WebApp.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class OrdersController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly ICustomerService _customerService;
        private readonly IProductService _productService;
        private readonly IStockService _stockService;

        public const string SessionKeyName = "_ItemCart";

        public OrdersController(IOrderService orderService, ICustomerService customerService, IProductService productService, IStockService stockService)
        {
            _orderService = orderService;
            _customerService = customerService;
            _productService = productService;
            _stockService = stockService;
        }

        public IActionResult Index()
        {
            var orders = _orderService.GetAllOrders().Result;
            var viewModel = new OrderHomePageViewModel { Orders = orders };
            return View(viewModel);
        }

        public IActionResult Details(string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest();
            var order = _orderService.GetDetails(id).Result;
            if (order == null)
                return NotFound();

            var viewModel = new OrderDetailsPageViewModel { Order = order };

            return View(viewModel);
        }

        public IActionResult New(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return BadRequest();

            var statusTypes = _orderService.GetAllStatusTypes().Result;
            var customer = _customerService.GetCustomerById(id).Result;
            var products = _productService.GetAllAsync().Result;
            var stocks = _stockService.GetAllAsync().Result;

            var viewModel = new AddOrderPageViewModel
            {
                OrderForm = new OrderFormViewModel(),
                OrderStatusTypes = statusTypes,
                Customer = customer,
                Products = products,
                Stocks = stocks,
                OrderItemList = new CartItemListDisplayViewModel()
            };
            return View(viewModel);
        }

        public IActionResult Edit(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return BadRequest();

            var order = _orderService.GetOrderToEdit(id).Result;
            if (order == null)
                return BadRequest();

            var statusTypes = _orderService.GetAllStatusTypes().Result;            
            var products = _productService.GetAllAsync().Result;

            var viewModel = new EditOrderPageViewModel { OrderForm = order, OrderStatusTypes = statusTypes, Products = products };
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Save(OrderFormViewModel orderForm)
        {
            // Cart from session
            var cartItemsList = HttpContext.Session.GetObject<IEnumerable<CartItemDto>>(SessionKeyName);
            if (cartItemsList == null)
                return BadRequest();

            var orderList = cartItemsList.Select(i => 
                new OrderItemDto 
                { 
                    ProductId = i.ProductId, 
                    Quantity = i.Quantity, 
                    Details = i.Description,
                    ItemTotalPrice = i.ItemTotalPrice
                }).ToList();

            orderForm.OrderItems = orderList;
            orderForm.OrderTotalPrice = orderList.Sum(i => i.ItemTotalPrice);
            
            var productId = _orderService.SaveOrder(orderForm).Result;

            if (productId != null)
                return RedirectToAction("Index", "Orders");

            return RedirectToAction("Index", "Orders");
        }

        [HttpPost]
        public IActionResult Update(OrderFormViewModel orderForm)
        {
            var result = _orderService.UpdateOrder(orderForm).Result;
            if (result)
                return RedirectToAction("Index", "Orders");

            return BadRequest();
        }

        [HttpPost]
        public IActionResult Delete(string orderId)
        {
            if (string.IsNullOrWhiteSpace(orderId))
                return BadRequest();

            var result = _orderService.DeleteOrder(orderId).Result;
            if(result)
                return RedirectToAction("Index", "Orders");

            return NotFound();
        }

        [HttpPost]
        public IActionResult AddItemToCartSession(string productId, int quantity, string description = null)
        {
            if (string.IsNullOrWhiteSpace(productId) || quantity < 1)
                return BadRequest();
            var product = _productService.GetById(productId).Result;

            var cartItem = new CartItemDto
            {
                ProductId = productId,
                Name = product.Name,
                Quantity = quantity,
                Description = description,
                ItemTotalPrice = product.UnitPrice * quantity
            };
            var cartItemsList = new List<CartItemDto>();

            if (HttpContext.Session.GetString(SessionKeyName) != null)
            {
                cartItemsList = HttpContext.Session.GetObject<List<CartItemDto>>(SessionKeyName);
            }
            
            cartItemsList.Add(cartItem);
            HttpContext.Session.SetObject(SessionKeyName, cartItemsList);

            return Ok();
        }

        [HttpGet]
        public IActionResult GetCartItems()
        {
            if (HttpContext.Session.GetString(SessionKeyName) == null)
                return BadRequest();

            var cartItemsList = HttpContext.Session.GetObject<List<CartItemDto>>(SessionKeyName);
            if (cartItemsList != null)
            {
                var viewModel = new CartItemListDisplayViewModel { CartItems = cartItemsList };
                return PartialView("_CartItemListPartial", viewModel);
            }
                

            return NotFound();
        }

        [HttpPost]
        public IActionResult RemoveCartItem(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return BadRequest();

            if (HttpContext.Session.GetString(SessionKeyName) == null)
                return NotFound();

            var cartItemsList = HttpContext.Session.GetObject<List<CartItemDto>>(SessionKeyName);            
            cartItemsList = cartItemsList.Where(i => i.ProductId != id).ToList();            
            if(cartItemsList != null)
            {
                HttpContext.Session.Remove(SessionKeyName);
                HttpContext.Session.SetObject<IEnumerable<CartItemDto>>(SessionKeyName, cartItemsList);

                var viewModel = new CartItemListDisplayViewModel { CartItems = cartItemsList };
                return PartialView("_CartItemListPartial", viewModel);
            }

            return NotFound();
        }

    }
}