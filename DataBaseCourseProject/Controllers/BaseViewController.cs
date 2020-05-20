using DataBaseCourseProject.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace DataBaseCourseProject.Controllers
{
    public class BaseViewController<T> : Controller
    {
        private readonly IViewService<T> viewService;

        public BaseViewController(IViewService<T> viewService)
        {
            this.viewService = viewService;
        }

        public IActionResult Index()
        {
            return View(viewService.GetView());
        }
    }
}