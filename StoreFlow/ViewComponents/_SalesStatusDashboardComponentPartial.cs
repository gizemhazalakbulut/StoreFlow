using Microsoft.AspNetCore.Mvc;
using StoreFlow.Context;
using StoreFlow.Models;

namespace StoreFlow.ViewComponents
{
    public class _SalesStatusDashboardComponentPartial:ViewComponent
    {
        private readonly StoreContext _context;
        public _SalesStatusDashboardComponentPartial(StoreContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            var data = _context.Customers
                .GroupBy(c => c.CustomerCity)
                .Select(g => new CustomerCityChartViewModel
                {
                    City = g.Key,
                    Count = g.Count()
                }).ToList();
            return View(data);
        }
    }
}
