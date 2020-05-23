using DataBaseCourseProject.ComponentInterfaces;
using DataBaseCourseProject.Models.Tables;
using DataBaseCourseProject.ServiceInterfaces;
using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DataBaseCourseProject.Services
{
    public class ShoppingCartDetailsTableService : ITableService<ShoppingCartDetails>
    {
        private readonly IOracleComponent oracleComponent;

        public ShoppingCartDetailsTableService(IOracleComponent oracleComponent)
        {
            this.oracleComponent = oracleComponent;
        }

        public BaseListModel<ShoppingCartDetails> GetPart(int startRow = 1)
        {
            var connection = oracleComponent.GetOpenConnection();
            var baseListModel = new BaseListModel<ShoppingCartDetails>
            {
                Entities = GetList(oracleComponent.CommandForGetPart(connection, "ShoppingCartDetails", startRow)),
                EntitiesCount = oracleComponent.GetRowsCount(connection, "ShoppingCartDetails")
            };

            connection.Dispose();
            return baseListModel;
        }

        public void Create(ShoppingCartDetails model)
        {
            var connection = oracleComponent.GetOpenConnection();
            var command = oracleComponent.GetCommand(connection, "AddShoppingCartDetails", CommandType.StoredProcedure);
            oracleComponent.AddParameter(command, "ShoppingCartIdVar", OracleDbType.Int32, model.ShoppingCartId);
            oracleComponent.AddParameter(command, "ProductIdVar", OracleDbType.Int32, model.ProductId);
            oracleComponent.AddParameter(command, "QuantityVar", OracleDbType.Int32, model.Quantity);
            command.ExecuteNonQuery();
            command.Dispose();
            connection.Close();
        }

        public void Delete(int id)
        {
            var connection = oracleComponent.GetOpenConnection();
            var command = oracleComponent.GetCommand(connection, "DeleteShoppingCartDetails", CommandType.StoredProcedure);
            oracleComponent.AddParameter(command, "IdVar", OracleDbType.Int32, id);
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void Update(ShoppingCartDetails model)
        {
            var connection = oracleComponent.GetOpenConnection();
            var command = oracleComponent.GetCommand(connection, "UpdateShoppingCartDetails", CommandType.StoredProcedure);
            oracleComponent.AddParameter(command, "IdVar", OracleDbType.Int32, model.ShoppingCartId);
            oracleComponent.AddParameter(command, "ShoppingCartIdVar", OracleDbType.Int32, model.ShoppingCartId);
            oracleComponent.AddParameter(command, "ProductIdVar", OracleDbType.Int32, model.ProductId);
            oracleComponent.AddParameter(command, "QuantityVar", OracleDbType.Int32, model.Quantity);
            command.ExecuteNonQuery();
            command.Dispose();
            connection.Close();
        }

        public ShoppingCartDetails GetById(int id)
        {
            var connection = oracleComponent.GetOpenConnection();
            var command = oracleComponent.GetCommand(connection, "select * from shoppingCartDetails where id = :idVar", CommandType.Text);
            oracleComponent.AddParameter(command, "idVar", OracleDbType.Int32, id);
            OracleDataReader dataReader = command.ExecuteReader();
            var shoppingCartDetails = new ShoppingCartDetails();
            while (dataReader.Read())
            {
                shoppingCartDetails.Id = dataReader.GetInt32(0);
                shoppingCartDetails.ShoppingCartId = dataReader.GetInt32(1);
                shoppingCartDetails.ProductId = dataReader.GetInt32(2);
                shoppingCartDetails.Quantity = dataReader.GetInt32(3);
            }

            dataReader.Close();
            connection.Dispose();
            return shoppingCartDetails;
        }

        public ShoppingCartDetails GetEmpty()
        {
            return new ShoppingCartDetails();
        }

        private List<ShoppingCartDetails> GetList(OracleCommand command)
        {
            OracleDataReader dataReader = command.ExecuteReader();
            var shoppingCartDetailsList = new List<ShoppingCartDetails>();
            while (dataReader.Read())
            {
                shoppingCartDetailsList.Add(new ShoppingCartDetails
                {
                    Id = dataReader.GetInt32(0),
                    ShoppingCartId = dataReader.GetInt32(1),
                    ProductId = dataReader.GetInt32(2),
                    Quantity = dataReader.GetInt32(3),
                    RowNum = dataReader.GetInt32(4)
                });
            }

            return shoppingCartDetailsList.OrderBy(x => x.Id).ToList();
        }
    }
}