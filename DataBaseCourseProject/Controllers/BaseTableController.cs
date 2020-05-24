using DataBaseCourseProject.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

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
            return View(tableService.GetPart());
        }

        public IActionResult GetItems(int startRow)
        {
            var model = tableService.GetPart(startRow);
            if (model.Entities == null)
            {
                model.Entities = new List<T>();
            }
            return PartialView("_Items", model);
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
            return View("Index", tableService.GetPart());
        }

        public IActionResult Delete(int id)
        {
            tableService.Delete(id);
            return View("Index", tableService.GetPart());
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
            return View("Index", tableService.GetPart());
        }
    }
}