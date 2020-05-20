using DataBaseCourseProject.Models.Tables;
using DataBaseCourseProject.ServiceInterfaces;

namespace DataBaseCourseProject.Controllers
{
    public class CategoryTableController : BaseTableController<Category>
    {
        public CategoryTableController(ITableService<Category> tableService) : base(tableService)
        {
        }
    }
}