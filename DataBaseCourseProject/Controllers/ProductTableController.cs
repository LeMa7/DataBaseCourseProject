using DataBaseCourseProject.Models.Tables;
using DataBaseCourseProject.ServiceInterfaces;

namespace DataBaseCourseProject.Controllers
{
    public class ProductTableController : BaseTableController<Product>
    {
        public ProductTableController(ITableService<Product> tableService) : base(tableService)
        {
        }
    }
}