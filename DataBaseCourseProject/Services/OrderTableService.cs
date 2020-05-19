using DataBaseCourseProject.ComponentInterfaces;
using DataBaseCourseProject.Models;
using DataBaseCourseProject.ServiceInterfaces;
using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;
using System.Data;

namespace DataBaseCourseProject.Services
{
    public class OrderTableService : ITableService<Order>
    {
        private readonly IOracleComponent oracleComponent;

        public OrderTableService(IOracleComponent oracleComponent)
        {
            this.oracleComponent = oracleComponent;
        }

        public List<Order> GetAll()
        {
            var connection = oracleComponent.GetOpenConnection();
            var command = oracleComponent.GetCommand(connection, "select * from orders", CommandType.Text);
            OracleDataReader dataReader = command.ExecuteReader();
            var orderList = new List<Order>();
            while (dataReader.Read())
            {
                orderList.Add(new Order
                {
                    Id = dataReader.GetInt32(0),
                    UserId = dataReader.GetInt32(1),
                    OrderDate = dataReader.GetDateTime(2),
                    ShipDate = dataReader.GetDateTime(3),
                    Adress = dataReader.GetString(4)
                });
            }

            dataReader.Close();
            connection.Dispose();
            return orderList;
        }

        public void Create(Order model)
        {
            var connection = oracleComponent.GetOpenConnection();
            var command = oracleComponent.GetCommand(connection, "AddOrder", CommandType.StoredProcedure);
            oracleComponent.AddParameter(command, "UserIdVar", OracleDbType.Int32, model.UserId);
            oracleComponent.AddParameter(command, "OrderDateVar", OracleDbType.Date, model.OrderDate);
            oracleComponent.AddParameter(command, "ShipDateVar", OracleDbType.Date, model.ShipDate);
            oracleComponent.AddParameter(command, "AdressVar", OracleDbType.Varchar2, model.Adress);
            command.ExecuteNonQuery();
            command.Dispose();
            connection.Close();
        }

        public void Delete(int id)
        {
            var connection = oracleComponent.GetOpenConnection();
            var command = oracleComponent.GetCommand(connection, "DeleteOrder", CommandType.StoredProcedure);
            oracleComponent.AddParameter(command, "IdVar", OracleDbType.Int32, id);
            command.ExecuteNonQuery();
            connection.Close();
        }

        public Order GetEmpty()
        {
            return new Order();
        }
    }
}