using DataBaseCourseProject.Models;
using DataBaseCourseProject.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace DataBaseCourseProject.Controllers
{
    public class UserTableController : BaseTableController<User>
    {
        public UserTableController(ITableService<User> tableService) : base(tableService)
        {
        }
    }
}