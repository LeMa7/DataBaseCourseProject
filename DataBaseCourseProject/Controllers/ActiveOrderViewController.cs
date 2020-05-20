using DataBaseCourseProject.Models.Views;
using DataBaseCourseProject.ServiceInterfaces;

namespace DataBaseCourseProject.Controllers
{
    public class ActiveOrderViewController : BaseViewController<ActiveOrderView>
    {
        public ActiveOrderViewController(IViewService<ActiveOrderView> tableService) : base(tableService)
        {
        }
    }
}