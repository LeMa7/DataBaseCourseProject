using DataBaseCourseProject.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace DataBaseCourseProject.Controllers
{
    public class ViewController : Controller
    {
        private readonly IViewService viewService;

        public ViewController(IViewService viewService)
        {
            this.viewService = viewService;
        }

        public IActionResult ActiveOrdersView()
        {
            return View(viewService.GetActiveOrdersView());
        }
    }
}