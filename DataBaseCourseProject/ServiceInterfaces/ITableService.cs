using System.Collections.Generic;

namespace DataBaseCourseProject.ServiceInterfaces
{
    public interface ITableService<T>
    {
        List<T> GetAll();

        void Create(T model);

        void Delete(int id);

        void Update(T model);

        T GetEmpty();

        T GetById(int id);
    }
}