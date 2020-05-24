using DataBaseCourseProject.Models.Action;
using DataBaseCourseProject.Models.Tables;
using DataBaseCourseProject.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace DataBaseCourseProject.Controllers
{
    public class ActionController : Controller
    {
        private readonly IActionService actionService;
        private readonly ITableService<Product> tableService;

        public ActionController(IActionService actionService, ITableService<Product> tableService)
        {
            this.actionService = actionService;
            this.tableService = tableService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetDifference(DifferenceModel model)
        {
            if (model.FirstId != null && model.SecondId != null)
            {
                var returnModel = new DifferenceModel
                {
                    Differences = actionService.GetDiff(model.FirstId, model.SecondId),
                    Produts = new List<Product>
                    {
                        tableService.GetById(model.FirstId.Value),
                        tableService.GetById(model.SecondId.Value)
                    }
                };

                return View(returnModel);
            }

            return View(new DifferenceModel
            {
                FirstId = model.FirstId,
                SecondId = model.SecondId,
                Produts = new List<Product>()
            });
        }
    }
}