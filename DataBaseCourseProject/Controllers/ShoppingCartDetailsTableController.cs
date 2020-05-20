using DataBaseCourseProject.Models;
using DataBaseCourseProject.ServiceInterfaces;

namespace DataBaseCourseProject.Controllers
{
    public class ShoppingCartDetailsTableController : BaseTableController<ShoppingCartDetails>
    {
        public ShoppingCartDetailsTableController(ITableService<ShoppingCartDetails> tableService) : base(tableService)
        {
        }
    }
}