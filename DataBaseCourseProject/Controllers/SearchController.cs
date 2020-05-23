using DataBaseCourseProject.Models.Search;
using DataBaseCourseProject.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace DataBaseCourseProject.Controllers
{
    public class SearchController : Controller
    {
        private readonly ISearchService searchService;

        public SearchController(ISearchService searchService)
        {
            this.searchService = searchService;
        }

        public IActionResult Index(string searchString)
        {
            var products = new List<ProductViewModel>();

            if (!string.IsNullOrEmpty(searchString))
            {
                products = searchService.FindProducts(searchString);
            }

            return View(products);
        }
    }
}