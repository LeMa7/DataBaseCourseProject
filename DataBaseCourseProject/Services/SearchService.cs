using DataBaseCourseProject.ComponentInterfaces;
using DataBaseCourseProject.Models.Search;
using DataBaseCourseProject.ServiceInterfaces;
using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;
using System.Data;

namespace DataBaseCourseProject.Services
{
    public class SearchService : ISearchService
    {
        private readonly IOracleComponent oracleComponent;

        public SearchService(IOracleComponent oracleComponent)
        {
            this.oracleComponent = oracleComponent;
        }

        public List<ProductViewModel> FindProducts(string searchString)
        {
            var connection = oracleComponent.GetOpenConnection();
            searchString = "%" + searchString + "%";
            var command = oracleComponent.GetCommand(connection, "select * from ProductsFullInfo where name like :searchString or producer like :searchString", CommandType.Text);
            oracleComponent.AddParameter(command, "searchString", OracleDbType.Varchar2, searchString);
            OracleDataReader dataReader = command.ExecuteReader();
            var productViewModelList = new List<ProductViewModel>();
            while (dataReader.Read())
            {
                productViewModelList.Add(new ProductViewModel
                {
                    Id = dataReader.GetInt32(0),
                    Name = dataReader.GetString(1),
                    Producer = dataReader.GetString(2),
                    Description = dataReader.GetString(3),
                    Category = dataReader.GetString(4),
                    Subcategory = dataReader.GetString(5),
                    Price = dataReader.GetInt32(6),
                    Quantity = dataReader.GetInt32(7)
                });
            }

            dataReader.Close();
            connection.Dispose();
            return productViewModelList;
        }
    }
}