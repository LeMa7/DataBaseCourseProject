using DataBaseCourseProject.Models;
using System.Collections.Generic;

namespace DataBaseCourseProject.ServiceInterfaces
{
    public interface IProducerTableService
    {
        List<Producer> GetAll();

        void CreateProducer(ProducerCreateModel model);

        void DeleteProducer(int id);
    }
}