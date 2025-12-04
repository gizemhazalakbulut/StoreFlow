using Microsoft.AspNetCore.Mvc;
using StoreFlow.Context;

namespace StoreFlow.ViewComponents
{
    public class _Last5ProductsDashboardComponentPartial:ViewComponent
    {
        private readonly StoreContext _context;
        public _Last5ProductsDashboardComponentPartial(Context.StoreContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            var values = _context.Products.OrderBy(x => x.ProductId).ToList().SkipLast(5).ToList().TakeLast(6).ToList();
            return View(values);
        }
    }
}
