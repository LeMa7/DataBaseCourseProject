using DataBaseCourseProject.Models.Views;
using System.Collections.Generic;

namespace DataBaseCourseProject.ServiceInterfaces
{
    public interface IViewService
    {
        List<ActiveOrderView> GetActiveOrdersView();
    }
}