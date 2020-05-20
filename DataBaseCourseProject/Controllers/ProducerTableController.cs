using DataBaseCourseProject.Models.Tables;
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