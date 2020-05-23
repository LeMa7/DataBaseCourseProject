using DataBaseCourseProject.ComponentInterfaces;
using DataBaseCourseProject.Models.Tables;
using DataBaseCourseProject.ServiceInterfaces;
using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DataBaseCourseProject.Services
{
    public class OrderDetailsTableService : ITableService<OrderDetails>
    {
        private readonly IOracleComponent oracleComponent;

        public OrderDetailsTableService(IOracleComponent oracleComponent)
        {
            this.oracleComponent = oracleComponent;
        }

        public BaseListModel<OrderDetails> GetPart(int startRow = 1)
        {
            var connection = oracleComponent.GetOpenConnection();
            var baseListModel = new BaseListModel<OrderDetails>
            {
                Entities = GetList(oracleComponent.CommandForGetPart(connection, "OrderDetails", startRow)),
                EntitiesCount = oracleComponent.GetRowsCount(connection, "OrderDetails")
            };

            connection.Dispose();
            return baseListModel;
        }

        public void Create(OrderDetails model)
        {
            var connection = oracleComponent.GetOpenConnection();
            var command = oracleComponent.GetCommand(connection, "AddOrderDetails", CommandType.StoredProcedure);
            oracleComponent.AddParameter(command, "OrderIdVar", OracleDbType.Int32, model.OrderId);
            oracleComponent.AddParameter(command, "ProductIdVar", OracleDbType.Int32, model.ProductId);
            oracleComponent.AddParameter(command, "QuantityVar", OracleDbType.Int32, model.Quantity);
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

        public void Update(OrderDetails model)
        {
            var connection = oracleComponent.GetOpenConnection();
            var command = oracleComponent.GetCommand(connection, "UpdateOrderDetails", CommandType.StoredProcedure);
            oracleComponent.AddParameter(command, "IdVar", OracleDbType.Int32, model.Id);
            oracleComponent.AddParameter(command, "OrderIdVar", OracleDbType.Int32, model.OrderId);
            oracleComponent.AddParameter(command, "ProductIdVar", OracleDbType.Int32, model.ProductId);
            oracleComponent.AddParameter(command, "QuantityVar", OracleDbType.Int32, model.Quantity);
            command.ExecuteNonQuery();
            command.Dispose();
            connection.Close();
        }

        public OrderDetails GetById(int id)
        {
            var connection = oracleComponent.GetOpenConnection();
            var command = oracleComponent.GetCommand(connection, "select * from orderDetails where id = :idVar", CommandType.Text);
            oracleComponent.AddParameter(command, "idVar", OracleDbType.Int32, id);
            OracleDataReader dataReader = command.ExecuteReader();
            var orderDetails = new OrderDetails();
            while (dataReader.Read())
            {
                orderDetails.Id = dataReader.GetInt32(0);
                orderDetails.OrderId = dataReader.GetInt32(1);
                orderDetails.ProductId = dataReader.GetInt32(2);
                orderDetails.Quantity = dataReader.GetInt32(3);
            }

            dataReader.Close();
            connection.Dispose();
            return orderDetails;
        }

        public OrderDetails GetEmpty()
        {
            return new OrderDetails();
        }

        private List<OrderDetails> GetList(OracleCommand command)
        {
            OracleDataReader dataReader = command.ExecuteReader();
            var orderDetailsList = new List<OrderDetails>();
            while (dataReader.Read())
            {
                orderDetailsList.Add(new OrderDetails
                {
                    Id = dataReader.GetInt32(0),
                    OrderId = dataReader.GetInt32(1),
                    ProductId = dataReader.GetInt32(2),
                    Quantity = dataReader.GetInt32(3),
                    RowNum = dataReader.GetInt32(4)
                });
            }

            return orderDetailsList.OrderBy(x => x.Id).ToList();
        }
    }
}