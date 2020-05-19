using DataBaseCourseProject.ComponentInterfaces;
using DataBaseCourseProject.Models;
using DataBaseCourseProject.ServiceInterfaces;
using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;
using System.Data;

namespace DataBaseCourseProject.Services
{
    public class OrderDetailsTableService : ITableService<OrderDetails>
    {
        private readonly IOracleComponent oracleComponent;

        public OrderDetailsTableService(IOracleComponent oracleComponent)
        {
            this.oracleComponent = oracleComponent;
        }

        public List<OrderDetails> GetAll()
        {
            var connection = oracleComponent.GetOpenConnection();
            var command = oracleComponent.GetCommand(connection, "select * from orderDetails", CommandType.Text);
            OracleDataReader dataReader = command.ExecuteReader();
            var orderDetailsList = new List<OrderDetails>();
            while (dataReader.Read())
            {
                orderDetailsList.Add(new OrderDetails
                {
                    Id = dataReader.GetInt32(0),
                    OrderId = dataReader.GetInt32(1),
                    ProductId = dataReader.GetInt32(2),
                    Quantity = dataReader.GetInt32(3)
                });
            }

            dataReader.Close();
            connection.Dispose();
            return orderDetailsList;
        }

        public void Create(OrderDetails model)
        {
            var connection = oracleComponent.GetOpenConnection();
            var command = oracleComponent.GetCommand(connection, "AddOrderDetails", CommandType.StoredProcedure);
            oracleComponent.AddParameter(command, "OrderIdVar", OracleDbType.Varchar2, model.OrderId);
            oracleComponent.AddParameter(command, "ProductIdVar", OracleDbType.Varchar2, model.ProductId);
            oracleComponent.AddParameter(command, "QuantityVar", OracleDbType.Varchar2, model.Quantity);
            command.ExecuteNonQuery();
            command.Dispose();
            connection.Close();
        }

        public void Delete(int id)
        {
            var connection = oracleComponent.GetOpenConnection();
            var command = oracleComponent.GetCommand(connection, "DeleteOrderDetails", CommandType.StoredProcedure);
            oracleComponent.AddParameter(command, "IdVar", OracleDbType.Int32, id);
            command.ExecuteNonQuery();
            connection.Close();
        }

        public OrderDetails GetEmpty()
        {
            return new OrderDetails();
        }
    }
}