using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MobieStoreWeb.Data;

namespace MobieStoreWeb.Areas.Administrator.Controllers
{
    [Area("Administrator")]
    [Authorize(Roles = "Admin,Employee")]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        // GET: /Administrator/Home/GetRevenueMonths
        public IActionResult GetRevenueMonths()
        {
            var toDate = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month + 1, 1); //VD: 2020/6/1
            var fromDate = toDate.AddMonths(-12).AddDays(-1); // 2019/5/31
            var orders = _context.Orders.Where(o => o.OrderDate > fromDate && o.OrderDate < toDate).Include(o => o.OrderDetails);
            var groupingMonths = orders.GroupBy(o => o.OrderDate.Month)
                                .Select(grouping => new
                                {
                                    Month = grouping.Key,
                                    Orders = grouping.Select(o => o)
                                });
            var revenueMonths = groupingMonths.Select(grouping => new
            {
                grouping.Month,
                Revenue = grouping.Orders.Sum(o => o.OrderDetails.Sum(od => od.Price * od.Quantity))
            });
            return Ok(revenueMonths);
        }
    }
}