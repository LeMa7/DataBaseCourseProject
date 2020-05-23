using DataBaseCourseProject.ComponentInterfaces;
using DataBaseCourseProject.Models.Tables;
using DataBaseCourseProject.ServiceInterfaces;
using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DataBaseCourseProject.Services
{
    public class CategoryTableService : ITableService<Category>
    {
        private readonly IOracleComponent oracleComponent;

        public CategoryTableService(IOracleComponent oracleComponent)
        {
            this.oracleComponent = oracleComponent;
        }

        public BaseListModel<Category> GetPart(int startRow = 1)
        {
            var connection = oracleComponent.GetOpenConnection();
            var baseListModel = new BaseListModel<Category>
            {
                Entities = GetList(oracleComponent.CommandForGetPart(connection, "Categories", startRow)),
                EntitiesCount = oracleComponent.GetRowsCount(connection, "Categories")
            };

            connection.Dispose();
            return baseListModel;
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

        public void Update(Category model)
        {
            var connection = oracleComponent.GetOpenConnection();
            var command = oracleComponent.GetCommand(connection, "UpdateCategory", CommandType.StoredProcedure);
            oracleComponent.AddParameter(command, "IdVar", OracleDbType.Int32, model.Id);
            oracleComponent.AddParameter(command, "NameVar", OracleDbType.Varchar2, model.Name);
            command.ExecuteNonQuery();
            command.Dispose();
            connection.Close();
        }

        public Category GetById(int id)
        {
            var connection = oracleComponent.GetOpenConnection();
            var command = oracleComponent.GetCommand(connection, "select * from categories where id = :idVar", CommandType.Text);
            oracleComponent.AddParameter(command, "idVar", OracleDbType.Int32, id);
            OracleDataReader dataReader = command.ExecuteReader();
            var category = new Category();
            while (dataReader.Read())
            {
                category.Id = dataReader.GetInt32(0);
                category.Name = dataReader.GetString(1);
            }

            dataReader.Close();
            connection.Dispose();
            return category;
        }

        public Category GetEmpty()
        {
            return new Category();
        }

        private List<Category> GetList(OracleCommand command)
        {
            OracleDataReader dataReader = command.ExecuteReader();
            var categoryList = new List<Category>();
            while (dataReader.Read())
            {
                categoryList.Add(new Category
                {
                    Id = dataReader.GetInt32(0),
                    Name = dataReader.GetString(1),
                    RowNum = dataReader.GetInt32(2)
                });
            }

            return categoryList.OrderBy(x => x.Id).ToList();
        }
    }
}