using DataBaseCourseProject.ComponentInterfaces;
using DataBaseCourseProject.Models.Views;
using DataBaseCourseProject.ServiceInterfaces;
using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;
using System.Data;

namespace DataBaseCourseProject.Services
{
    public class ActiveOrderViewService : IViewService<ActiveOrderView>
    {
        private readonly IOracleComponent oracleComponent;

        public ActiveOrderViewService(IOracleComponent oracleComponent)
        {
            this.oracleComponent = oracleComponent;
        }

        public List<ActiveOrderView> GetView()
        {
            var connection = oracleComponent.GetOpenConnection();
            var command = oracleComponent.GetCommand(connection, "select * from ActiveOrdersView", CommandType.Text);
            OracleDataReader dataReader = command.ExecuteReader();
            var activeOrderViewList = new List<ActiveOrderView>();
            while (dataReader.Read())
            {
                activeOrderViewList.Add(new ActiveOrderView
                {
                    UserId = dataReader.GetInt32(0),
                    FirstName = dataReader.GetString(1),
                    LastName = dataReader.GetString(2),
                    MiddleName = dataReader.GetString(3),
                    OrderDate = dataReader.GetDateTime(4),
                    ShipDate = dataReader.GetDateTime(5),
                    Adress = dataReader.GetString(6)
                });
            }

            dataReader.Close();
            connection.Dispose();
            return activeOrderViewList;
        }
    }
}