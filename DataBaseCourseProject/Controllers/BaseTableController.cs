using DataBaseCourseProject.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace DataBaseCourseProject.Controllers
{
    public class BaseTableController<T> : Controller
    {
        private readonly ITableService<T> tableService;

        public BaseTableController(ITableService<T> tableService)
        {
            this.tableService = tableService;
        }

        public IActionResult Index()
        {
            return View(tableService.GetAll());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(tableService.GetEmpty());
        }

        [HttpPost]
        public IActionResult Create(T model)
        {
            tableService.Create(model);
            return View("Index", tableService.GetAll());
        }

        public IActionResult Delete(int id)
        {
            tableService.Delete(id);
            return View("Index", tableService.GetAll());
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            return View("Create", tableService.GetById(id));
        }

        [HttpPost]
        public IActionResult Update(T model)
        {
            tableService.Update(model);
            return View("Index", tableService.GetAll());
        }
    }
}