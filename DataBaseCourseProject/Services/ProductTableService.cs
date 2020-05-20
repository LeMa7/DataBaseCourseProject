﻿using DataBaseCourseProject.ComponentInterfaces;
using DataBaseCourseProject.Models;
using DataBaseCourseProject.ServiceInterfaces;
using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;
using System.Data;

namespace DataBaseCourseProject.Services
{
    public class ProductTableService : ITableService<Product>
    {
        private readonly IOracleComponent oracleComponent;

        public ProductTableService(IOracleComponent oracleComponent)
        {
            this.oracleComponent = oracleComponent;
        }

        public List<Product> GetAll()
        {
            var connection = oracleComponent.GetOpenConnection();
            var command = oracleComponent.GetCommand(connection, "select * from products", CommandType.Text);
            OracleDataReader dataReader = command.ExecuteReader();
            var productList = new List<Product>();
            while (dataReader.Read())
            {
                productList.Add(new Product
                {
                    Id = dataReader.GetInt32(0),
                    Name = dataReader.GetString(1),
                    Description = dataReader.GetString(2),
                    SubcategoryId = dataReader.GetInt32(3),
                    ProducerId = dataReader.GetInt32(4),
                    Price = dataReader.GetInt32(5),
                    Quantity = dataReader.GetInt32(6)
                });
            }

            dataReader.Close();
            connection.Dispose();
            return productList;
        }

        public void Create(Product model)
        {
            var connection = oracleComponent.GetOpenConnection();
            var command = oracleComponent.GetCommand(connection, "AddProduct", CommandType.StoredProcedure);
            oracleComponent.AddParameter(command, "NameVar", OracleDbType.Varchar2, model.Name);
            oracleComponent.AddParameter(command, "DescriptionVar", OracleDbType.Varchar2, model.Description);
            oracleComponent.AddParameter(command, "SubcategoryIdVar", OracleDbType.Varchar2, model.SubcategoryId);
            oracleComponent.AddParameter(command, "ProducerIdVar", OracleDbType.Varchar2, model.ProducerId);
            oracleComponent.AddParameter(command, "PriceVar", OracleDbType.Varchar2, model.Price);
            oracleComponent.AddParameter(command, "QuantityVar", OracleDbType.Varchar2, model.Quantity);
            command.ExecuteNonQuery();
            command.Dispose();
            connection.Close();
        }

        public void Delete(int id)
        {
            var connection = oracleComponent.GetOpenConnection();
            var command = oracleComponent.GetCommand(connection, "DeleteProduct", CommandType.StoredProcedure);
            oracleComponent.AddParameter(command, "IdVar", OracleDbType.Int32, id);
            command.ExecuteNonQuery();
            connection.Close();
        }

        public Product GetEmpty()
        {
            return new Product();
        }
    }
}