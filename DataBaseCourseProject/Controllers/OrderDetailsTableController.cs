using DataBaseCourseProject.Models;
using DataBaseCourseProject.ServiceInterfaces;

namespace DataBaseCourseProject.Controllers
{
    public class OrderDetailsTableController : BaseTableController<OrderDetails>
    {
        public OrderDetailsTableController(ITableService<OrderDetails> tableService) : base(tableService)
        {
        }
    }
}