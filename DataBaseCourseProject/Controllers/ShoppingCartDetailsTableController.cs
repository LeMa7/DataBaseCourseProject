using DataBaseCourseProject.Models.Tables;
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