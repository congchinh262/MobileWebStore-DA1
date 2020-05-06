using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MobieStoreWeb.Data;
using MobieStoreWeb.Helpers;
using MobieStoreWeb.Models;
using MobieStoreWeb.ViewModels;

namespace MobieStoreWeb.Controllers
{
    public class CheckoutController : Controller
    {
        private const string SessionKeyCart = "_Cart";
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public CheckoutController(ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> Index()
        {
            var cart = HttpContext.Session.Get<CartViewModel>(SessionKeyCart);
            if (cart?.Items?.Count == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            var viewModel = new CheckoutViewModel
            {
                Cart = cart,
                IsGuest = true
            };
            if (_signInManager.IsSignedIn(User))
            {
                var user = await _userManager.GetUserAsync(User);
                viewModel.BillingName = user.Name;
                viewModel.BillingAddress = user.Address;
                viewModel.BillingPhoneNumber = user.PhoneNumber;
                viewModel.ShippingName = user.Name;
                viewModel.ShippingAddress = user.Address;
                viewModel.ShippingPhoneNumber = user.PhoneNumber;
                viewModel.IsGuest = false;
            }
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Index(CheckoutViewModel viewModel)
        {
            if (!viewModel.IsGuest && !_signInManager.IsSignedIn(User))
            {
                return RedirectToAction("Login", "Account");
            }

            viewModel.Cart = HttpContext.Session.Get<CartViewModel>(SessionKeyCart);
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            if (viewModel.Cart?.Items?.Count == 0)
            {
                return NotFound();
            }

            using (var transcation = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var order = new Order
                    {
                        BillingName = viewModel.BillingName,
                        BillingAddress = viewModel.BillingAddress,
                        BillingPhoneNumber = viewModel.BillingPhoneNumber,
                        ShippingName = viewModel.ShippingName,
                        ShippingAddress = viewModel.ShippingAddress,
                        ShippingPhoneNumber = viewModel.ShippingPhoneNumber,
                        DeliveryOption = viewModel.DeliveryOption,
                        PaymentMethod = viewModel.PaymentMethod,
                        OrderDate = DateTime.UtcNow,
                        Status = OrderStatus.Pending,
                        PaymentStatus = PaymentStatus.Pending,
                        UserId = viewModel.IsGuest ? null : _userManager.GetUserId(User)
                    };

                    var orderDetails = new List<OrderDetail>();
                    foreach (var item in viewModel.Cart.Items)
                    {
                        if (item.Quantity < 1)
                        {
                            item.Quantity = 1;
                        }
                        if (item.Quantity > 5)
                        {
                            item.Quantity = 5;
                        }

                        var product = _context.Products.Find(item.Id);
                        product.Quantity -= item.Quantity;
                        if (product.Quantity < 0 || !(product.Status == ProductStatus.Available))
                        {
                            // Trả 1 View("ErrorQuantity", $"Mặt hàng: {product.Name} đéo đủ số lượng ");
                            return BadRequest();
                        }

                        orderDetails.Add(new OrderDetail
                        {
                            ProductId = product.Id,
                            Price = product.Price,
                            Quantity = item.Quantity
                        });
                    }
                    order.OrderDetails = orderDetails;
                    _context.Orders.Add(order);
                    await _context.SaveChangesAsync();
                    await transcation.CommitAsync();
                    HttpContext.Session.Set<CartViewModel>(SessionKeyCart, null);
                }
                catch (Exception)
                {
                    await transcation.RollbackAsync();
                    return BadRequest();
                }
            }

            // Thiết kế lại
            return View("CheckoutCompleted");
        }

    }
}