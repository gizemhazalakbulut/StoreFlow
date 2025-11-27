using Microsoft.AspNetCore.Mvc;

namespace StoreFlow.ViewComponents
{
    public class _DailySalesDashboardComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
