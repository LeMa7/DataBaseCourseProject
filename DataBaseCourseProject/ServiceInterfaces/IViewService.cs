using System.Collections.Generic;

namespace DataBaseCourseProject.ServiceInterfaces
{
    public interface IViewService<T>
    {
        List<T> GetView();
    }
}