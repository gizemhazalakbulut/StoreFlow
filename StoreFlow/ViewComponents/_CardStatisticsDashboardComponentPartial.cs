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

            return View();
        }
    }
}
