using DataBaseCourseProject.Models.Search;
using System.Collections.Generic;

namespace DataBaseCourseProject.ServiceInterfaces
{
    public interface ISearchService
    {
        List<ProductViewModel> FindProducts(string searchString);
    }
}