﻿using DataBaseCourseProject.ComponentInterfaces;
using DataBaseCourseProject.Models.Tables;
using DataBaseCourseProject.ServiceInterfaces;
using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DataBaseCourseProject.Services
{
    public class SubcategoryTableService : ITableService<Subcategory>
    {
        private readonly IOracleComponent oracleComponent;

        public SubcategoryTableService(IOracleComponent oracleComponent)
        {
            this.oracleComponent = oracleComponent;
        }

        public BaseListModel<Subcategory> GetPart(int startRow = 1)
        {
            var connection = oracleComponent.GetOpenConnection();
            var baseListModel = new BaseListModel<Subcategory>
            {
                Entities = GetList(oracleComponent.CommandForGetPart(connection, "Subcategories", startRow)),
                EntitiesCount = oracleComponent.GetRowsCount(connection, "Subcategories")
            };

            connection.Dispose();
            return baseListModel;
        }

        public void Create(Subcategory model)
        {
            var connection = oracleComponent.GetOpenConnection();
            var command = oracleComponent.GetCommand(connection, "AddSubcategory", CommandType.StoredProcedure);
            oracleComponent.AddParameter(command, "NameVar", OracleDbType.Varchar2, model.Name);
            oracleComponent.AddParameter(command, "CategoryIdVar", OracleDbType.Int32, model.CategoryId);
            command.ExecuteNonQuery();
            command.Dispose();
            connection.Close();
        }

        public void Delete(int id)
        {
            var connection = oracleComponent.GetOpenConnection();
            var command = oracleComponent.GetCommand(connection, "DeleteSubcategory", CommandType.StoredProcedure);
            oracleComponent.AddParameter(command, "IdVar", OracleDbType.Int32, id);
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void Update(Subcategory model)
        {
            var connection = oracleComponent.GetOpenConnection();
            var command = oracleComponent.GetCommand(connection, "UpdateSubcategory", CommandType.StoredProcedure);
            oracleComponent.AddParameter(command, "IdVar", OracleDbType.Int32, model.Id);
            oracleComponent.AddParameter(command, "NameVar", OracleDbType.Varchar2, model.Name);
            oracleComponent.AddParameter(command, "CategoryIdVar", OracleDbType.Int32, model.CategoryId);
            command.ExecuteNonQuery();
            command.Dispose();
            connection.Close();
        }

        public Subcategory GetById(int id)
        {
            var connection = oracleComponent.GetOpenConnection();
            var command = oracleComponent.GetCommand(connection, "select * from subcategories where id = :idVar", CommandType.Text);
            oracleComponent.AddParameter(command, "idVar", OracleDbType.Int32, id);
            OracleDataReader dataReader = command.ExecuteReader();
            var subcategory = new Subcategory();
            while (dataReader.Read())
            {
                subcategory.Id = dataReader.GetInt32(0);
                subcategory.Name = dataReader.GetString(1);
                subcategory.CategoryId = dataReader.GetInt32(2);
            }

            dataReader.Close();
            connection.Dispose();
            return subcategory;
        }

        public Subcategory GetEmpty()
        {
            return new Subcategory();
        }

        private List<Subcategory> GetList(OracleCommand command)
        {
            OracleDataReader dataReader = command.ExecuteReader();
            var subcategoryList = new List<Subcategory>();
            while (dataReader.Read())
            {
                subcategoryList.Add(new Subcategory
                {
                    Id = dataReader.GetInt32(0),
                    Name = dataReader.GetString(1),
                    CategoryId = dataReader.GetInt32(2),
                    RowNum = dataReader.GetInt32(3)
                });
            }

            return subcategoryList.OrderBy(x => x.Id).ToList();
        }
    }
}