using DataBaseCourseProject.ComponentInterfaces;
using DataBaseCourseProject.Models;
using DataBaseCourseProject.ServiceInterfaces;
using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;
using System.Data;

namespace DataBaseCourseProject.Services
{
    public class CategoryTableService : ITableService<Category>
    {
        private readonly IOracleComponent oracleComponent;

        public CategoryTableService(IOracleComponent oracleComponent)
        {
            this.oracleComponent = oracleComponent;
        }

        public List<Category> GetAll()
        {
            var connection = oracleComponent.GetOpenConnection();
            var command = oracleComponent.GetCommand(connection, "select * from categories", CommandType.Text);
            OracleDataReader dataReader = command.ExecuteReader();
            var categoryList = new List<Category>();
            while (dataReader.Read())
            {
                categoryList.Add(new Category
                {
                    Id = dataReader.GetInt32(0),
                    Name = dataReader.GetString(1)
                });
            }

            dataReader.Close();
            connection.Dispose();
            return categoryList;
        }

        public void Create(Category model)
        {
            var connection = oracleComponent.GetOpenConnection();
            var command = oracleComponent.GetCommand(connection, "AddCategory", CommandType.StoredProcedure);
            oracleComponent.AddParameter(command, "NameVar", OracleDbType.Varchar2, model.Name);
            command.ExecuteNonQuery();
            command.Dispose();
            connection.Close();
        }

        public void Delete(int id)
        {
            var connection = oracleComponent.GetOpenConnection();
            var command = oracleComponent.GetCommand(connection, "DeleteCategory", CommandType.StoredProcedure);
            oracleComponent.AddParameter(command, "IdVar", OracleDbType.Int32, id);
            command.ExecuteNonQuery();
            connection.Close();
        }

        public Category GetEmpty()
        {
            return new Category();
        }
    }
}