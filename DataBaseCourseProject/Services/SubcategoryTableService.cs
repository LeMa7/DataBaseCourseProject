using DataBaseCourseProject.ComponentInterfaces;
using DataBaseCourseProject.Models;
using DataBaseCourseProject.ServiceInterfaces;
using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;
using System.Data;

namespace DataBaseCourseProject.Services
{
    public class SubcategoryTableService : ITableService<Subcategory>
    {
        private readonly IOracleComponent oracleComponent;

        public SubcategoryTableService(IOracleComponent oracleComponent)
        {
            this.oracleComponent = oracleComponent;
        }

        public List<Subcategory> GetAll()
        {
            var connection = oracleComponent.GetOpenConnection();
            var command = oracleComponent.GetCommand(connection, "select * from subcategories", CommandType.Text);
            OracleDataReader dataReader = command.ExecuteReader();
            var subcategoryList = new List<Subcategory>();
            while (dataReader.Read())
            {
                subcategoryList.Add(new Subcategory
                {
                    Id = dataReader.GetInt32(0),
                    Name = dataReader.GetString(1),
                    CategoryId = dataReader.GetInt32(2)
                });
            }

            dataReader.Close();
            connection.Dispose();
            return subcategoryList;
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

        public Subcategory GetEmpty()
        {
            return new Subcategory();
        }
    }
}