using DataBaseCourseProject.Models;
using DataBaseCourseProject.ServiceInterfaces;

namespace DataBaseCourseProject.Controllers
{
    public class ShoppingCartTableController : BaseTableController<ShoppingCart>
    {
        public ShoppingCartTableController(ITableService<ShoppingCart> tableService) : base(tableService)
        {
        }
    }
}