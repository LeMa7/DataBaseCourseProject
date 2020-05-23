using System.Collections.Generic;

namespace DataBaseCourseProject.Models.Tables
{
    public class BaseListModel<T>
    {
        public List<T> Entities { get; set; }

        public int EntitiesCount { get; set; }
    }
}