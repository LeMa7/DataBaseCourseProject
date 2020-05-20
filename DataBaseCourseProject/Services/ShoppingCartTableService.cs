﻿using DataBaseCourseProject.ComponentInterfaces;
using DataBaseCourseProject.Models.Tables;
using DataBaseCourseProject.ServiceInterfaces;
using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;
using System.Data;

namespace DataBaseCourseProject.Services
{
    public class ShoppingCartTableService : ITableService<ShoppingCart>
    {
        private readonly IOracleComponent oracleComponent;

        public ShoppingCartTableService(IOracleComponent oracleComponent)
        {
            this.oracleComponent = oracleComponent;
        }

        public List<ShoppingCart> GetAll()
        {
            var connection = oracleComponent.GetOpenConnection();
            var command = oracleComponent.GetCommand(connection, "select * from shoppingCarts", CommandType.Text);
            OracleDataReader dataReader = command.ExecuteReader();
            var shoppingCartList = new List<ShoppingCart>();
            while (dataReader.Read())
            {
                shoppingCartList.Add(new ShoppingCart
                {
                    Id = dataReader.GetInt32(0),
                    UserId = dataReader.GetInt32(1)
                });
            }

            dataReader.Close();
            connection.Dispose();
            return shoppingCartList;
        }

        public void Create(ShoppingCart model)
        {
            var connection = oracleComponent.GetOpenConnection();
            var command = oracleComponent.GetCommand(connection, "AddShoppingCart", CommandType.StoredProcedure);
            oracleComponent.AddParameter(command, "UserIdVar", OracleDbType.Int32, model.UserId);
            command.ExecuteNonQuery();
            command.Dispose();
            connection.Close();
        }

        public void Delete(int id)
        {
            var connection = oracleComponent.GetOpenConnection();
            var command = oracleComponent.GetCommand(connection, "DeleteShoppingCart", CommandType.StoredProcedure);
            oracleComponent.AddParameter(command, "IdVar", OracleDbType.Int32, id);
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void Update(ShoppingCart model)
        {
            var connection = oracleComponent.GetOpenConnection();
            var command = oracleComponent.GetCommand(connection, "UpdateShoppingCart", CommandType.StoredProcedure);
            oracleComponent.AddParameter(command, "IdVar", OracleDbType.Int32, model.Id);
            oracleComponent.AddParameter(command, "UserIdVar", OracleDbType.Int32, model.UserId);
            command.ExecuteNonQuery();
            command.Dispose();
            connection.Close();
        }

        public ShoppingCart GetById(int id)
        {
            var connection = oracleComponent.GetOpenConnection();
            var command = oracleComponent.GetCommand(connection, "select * from shoppingCarts where id = :idVar", CommandType.Text);
            oracleComponent.AddParameter(command, "idVar", OracleDbType.Int32, id);
            OracleDataReader dataReader = command.ExecuteReader();
            var shoppingCart = new ShoppingCart();
            while (dataReader.Read())
            {
                shoppingCart.Id = dataReader.GetInt32(0);
                shoppingCart.UserId = dataReader.GetInt32(1);
            }

            dataReader.Close();
            connection.Dispose();
            return shoppingCart;
        }

        public ShoppingCart GetEmpty()
        {
            return new ShoppingCart();
        }
    }
}