using DataBaseCourseProject.Models;
using DataBaseCourseProject.ServiceInterfaces;

namespace DataBaseCourseProject.Controllers
{
    public class ProducerTableController : BaseTableController<Producer>
    {
        public ProducerTableController(ITableService<Producer> tableService) : base(tableService)
        {
        }
    }
}