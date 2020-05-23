using DataBaseCourseProject.ComponentInterfaces;
using DataBaseCourseProject.Models.Tables;
using DataBaseCourseProject.ServiceInterfaces;
using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DataBaseCourseProject.Services
{
    public class OrderTableService : ITableService<Order>
    {
        private readonly IOracleComponent oracleComponent;

        public OrderTableService(IOracleComponent oracleComponent)
        {
            this.oracleComponent = oracleComponent;
        }

        public BaseListModel<Order> GetPart(int startRow = 1)
        {
            var connection = oracleComponent.GetOpenConnection();
            var baseListModel = new BaseListModel<Order>
            {
                Entities = GetList(oracleComponent.CommandForGetPart(connection, "Orders", startRow)),
                EntitiesCount = oracleComponent.GetRowsCount(connection, "Orders")
            };

            connection.Dispose();
            return baseListModel;
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

        public void Update(Order model)
        {
            var connection = oracleComponent.GetOpenConnection();
            var command = oracleComponent.GetCommand(connection, "UpdateOrder", CommandType.StoredProcedure);
            oracleComponent.AddParameter(command, "IdVar", OracleDbType.Int32, model.UserId);
            oracleComponent.AddParameter(command, "UserIdVar", OracleDbType.Int32, model.UserId);
            oracleComponent.AddParameter(command, "OrderDateVar", OracleDbType.Date, model.OrderDate);
            oracleComponent.AddParameter(command, "ShipDateVar", OracleDbType.Date, model.ShipDate);
            oracleComponent.AddParameter(command, "AdressVar", OracleDbType.Varchar2, model.Adress);
            command.ExecuteNonQuery();
            command.Dispose();
            connection.Close();
        }

        public Order GetById(int id)
        {
            var connection = oracleComponent.GetOpenConnection();
            var command = oracleComponent.GetCommand(connection, "select * from orders where id = :idVar", CommandType.Text);
            oracleComponent.AddParameter(command, "idVar", OracleDbType.Int32, id);
            OracleDataReader dataReader = command.ExecuteReader();
            var order = new Order();
            while (dataReader.Read())
            {
                order.Id = dataReader.GetInt32(0);
                order.UserId = dataReader.GetInt32(1);
                order.OrderDate = dataReader.GetDateTime(2);
                order.ShipDate = dataReader.GetDateTime(3);
                order.Adress = dataReader.GetString(4);
            }

            dataReader.Close();
            connection.Dispose();
            return order;
        }

        public Order GetEmpty()
        {
            return new Order();
        }

        private List<Order> GetList(OracleCommand command) 
        {
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
                    Adress = dataReader.GetString(4),
                    RowNum = dataReader.GetInt32(5)
                });
            }

            return orderList.OrderBy(x => x.Id).ToList();
        }
    }
}