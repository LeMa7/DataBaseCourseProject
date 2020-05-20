using DataBaseCourseProject.Models.Tables;
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