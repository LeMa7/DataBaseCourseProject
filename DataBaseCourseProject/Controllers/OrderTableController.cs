using DataBaseCourseProject.Models;
using DataBaseCourseProject.ServiceInterfaces;

namespace DataBaseCourseProject.Controllers
{
    public class OrderTableController : BaseTableController<Order>
    {
        public OrderTableController(ITableService<Order> tableService) : base(tableService)
        {
        }
    }
}