using DataBaseCourseProject.ComponentInterfaces;
using DataBaseCourseProject.Models.Tables;
using DataBaseCourseProject.ServiceInterfaces;
using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;
using System.Data;

namespace DataBaseCourseProject.Services
{
    public class ShoppingCartDetailsTableService : ITableService<ShoppingCartDetails>
    {
        private readonly IOracleComponent oracleComponent;

        public ShoppingCartDetailsTableService(IOracleComponent oracleComponent)
        {
            this.oracleComponent = oracleComponent;
        }

        public List<ShoppingCartDetails> GetAll()
        {
            var connection = oracleComponent.GetOpenConnection();
            var command = oracleComponent.GetCommand(connection, "select * from shoppingCartDetails", CommandType.Text);
            OracleDataReader dataReader = command.ExecuteReader();
            var shoppingCartDetailsList = new List<ShoppingCartDetails>();
            while (dataReader.Read())
            {
                shoppingCartDetailsList.Add(new ShoppingCartDetails
                {
                    Id = dataReader.GetInt32(0),
                    ShoppingCartId = dataReader.GetInt32(1),
                    ProductId = dataReader.GetInt32(2),
                    Quantity = dataReader.GetInt32(3)
                });
            }

            dataReader.Close();
            connection.Dispose();
            return shoppingCartDetailsList;
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

        public ShoppingCartDetails GetEmpty()
        {
            return new ShoppingCartDetails();
        }
    }
}