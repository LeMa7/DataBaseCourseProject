using DataBaseCourseProject.Models.Tables;
using System.Collections.Generic;

namespace DataBaseCourseProject.ServiceInterfaces
{
    public interface ITableService<T>
    {
        void Create(T model);

        void Delete(int id);

        void Update(T model);

        T GetEmpty();

        T GetById(int id);

        BaseListModel<T> GetPart(int startRow = 1);
    }
}