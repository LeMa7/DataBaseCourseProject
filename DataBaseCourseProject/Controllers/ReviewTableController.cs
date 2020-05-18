using DataBaseCourseProject.Models;
using DataBaseCourseProject.ServiceInterfaces;

namespace DataBaseCourseProject.Controllers
{
    public class ReviewTableController : BaseTableController<Review>
    {
        public ReviewTableController(ITableService<Review> tableService) : base(tableService)
        {
        }
    }
}