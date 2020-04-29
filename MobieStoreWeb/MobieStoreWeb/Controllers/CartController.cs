using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MobieStoreWeb.Data;
using MobieStoreWeb.Helpers;
using MobieStoreWeb.ViewModels;

namespace MobieStoreWeb.Controllers
{
    public class CartController : Controller
    {
        public const string SessionKeyCart = "_Cart";
        private readonly ApplicationDbContext _context;

        public CartController(ApplicationDbContext context)
        {
            _context = context;
        }
        
        [HttpPost]
        public IActionResult AddToCart(int id)
        {
            var product = _context.Products.Find(id);
            if(product == null)
            {
                return NotFound();
            }

            var cart = HttpContext.Session.Get<CartViewModel>(SessionKeyCart);
            if (cart == null)
            {
                cart = new CartViewModel();
            }

            var item = cart.Items.SingleOrDefault(i => i.Id == product.Id);
            if (item == null)
            {
                cart.Items.Add(new CartItemViewModel
                {
                    Id = product.Id,
                    Name=product.Name,
                    CategoryId=product.CategoryId,
                    ManufacturerId=product.ManufacturerId,
                    Price=product.Price,
                    Quantity=1,
                    Image=product.Image,
                });
            }
            else
            {
                item.Quantity++;
            }
            HttpContext.Session.Set(SessionKeyCart, cart);
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public IActionResult RemoveFromCart(int id)
        {
            var cart = HttpContext.Session.Get<CartViewModel>(SessionKeyCart);
            var item = cart.Items.SingleOrDefault(i => i.Id == id);
            cart.Items.Remove(item);
            HttpContext.Session.Set(SessionKeyCart, cart);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Index()
        {
            var cart = HttpContext.Session.Get<CartViewModel>(SessionKeyCart);
            if (cart == null)
            {
                cart = new CartViewModel();
                HttpContext.Session.Set(SessionKeyCart, cart);
            }
            return View(cart);
        }
    }
}