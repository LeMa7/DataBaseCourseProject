using DataBaseCourseProject.Models;
using DataBaseCourseProject.ServiceInterfaces;

namespace DataBaseCourseProject.Controllers
{
    public class SubcategoryTableController : BaseTableController<Subcategory>
    {
        public SubcategoryTableController(ITableService<Subcategory> tableService) : base(tableService)
        {
        }
    }
}