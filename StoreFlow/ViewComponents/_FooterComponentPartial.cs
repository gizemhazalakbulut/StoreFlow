using Microsoft.AspNetCore.Mvc;

namespace StoreFlow.ViewComponents
{
    public class _FooterComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }

    }
}
