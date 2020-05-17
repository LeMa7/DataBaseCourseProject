using DataBaseCourseProject.Models;
using DataBaseCourseProject.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace DataBaseCourseProject.Controllers
{
    public class ProducerTableController : Controller
    {
        private readonly IProducerTableService producerTableService;

        public ProducerTableController(IProducerTableService producerTableService)
        {
            this.producerTableService = producerTableService;
        }

        public IActionResult Index()
        {
            return View(producerTableService.GetAll());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new ProducerCreateModel());
        }

        [HttpPost]
        public IActionResult Create(ProducerCreateModel model)
        {
            producerTableService.CreateProducer(model);
            return View("Index", producerTableService.GetAll());
        }

        public IActionResult Delete(int id)
        {
            producerTableService.DeleteProducer(id);
            return View("Index", producerTableService.GetAll());
        }
    }
}