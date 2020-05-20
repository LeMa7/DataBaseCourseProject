using DataBaseCourseProject.Models.Tables;
using DataBaseCourseProject.ServiceInterfaces;

namespace DataBaseCourseProject.Controllers
{
    public class UserTableController : BaseTableController<User>
    {
        public UserTableController(ITableService<User> tableService) : base(tableService)
        {
        }
    }
}