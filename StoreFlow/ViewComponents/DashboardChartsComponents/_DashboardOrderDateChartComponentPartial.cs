using Microsoft.AspNetCore.Mvc;
using StoreFlow.Context;
using StoreFlow.Models;

namespace StoreFlow.ViewComponents.DashboardChartsComponents
{
    public class _DashboardOrderDateChartComponentPartial : ViewComponent
    {
        private readonly StoreContext _context;
        public _DashboardOrderDateChartComponentPartial(StoreContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            var data = _context.Orders
                .GroupBy(o => o.OrderDate.Date)
                .Select(g => new
                {
                    RawDate = g.Key,
                    Count = g.Count()
                })
                .OrderBy(y => y.RawDate)
                .ToList()
                .Select(d => new OrderDateViewModel
                {
                    Date = d.RawDate.ToString("yyyy-MM-dd"),
                    Count = d.Count
                }).ToList();


            return View(data);
        }
    }
}