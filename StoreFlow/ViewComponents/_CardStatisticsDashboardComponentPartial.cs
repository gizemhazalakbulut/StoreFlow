using Microsoft.AspNetCore.Mvc;
using StoreFlow.Context;

namespace StoreFlow.ViewComponents
{
    public class _CardStatisticsDashboardComponentPartial:ViewComponent
    {
        private readonly StoreContext _context;

        public _CardStatisticsDashboardComponentPartial(StoreContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            ViewBag.totalCustomerCount = _context.Customers.Count(); //Toplam Müşteri Sayısı
            ViewBag.totalCategoryCount = _context.Categories.Count(); //Toplam Kategori Sayısı
            ViewBag.totalProductCount = _context.Products.Count(); //Toplam Ürün Sayısı
            ViewBag.avgCustomerBalance = _context.
                Customers.Average(c => c.CustomerBalance).ToString("0.00");  //Ortalama Müşteri Bakiye
            ViewBag.totalOrderCount = _context.Orders.Count(); //Toplam Sipariş Sayısı
            ViewBag.sumOrderProductCount = _context.Orders.Sum(x=>x.OrderCount); //Toplam Sipariş Ürün Sayısı

            return View();
        }
    }
}
