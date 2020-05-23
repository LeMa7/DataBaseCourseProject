using DataBaseCourseProject.ComponentInterfaces;
using DataBaseCourseProject.Models.Tables;
using DataBaseCourseProject.ServiceInterfaces;
using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DataBaseCourseProject.Services
{
    public class ReviewTableService : ITableService<Review>
    {
        private readonly IOracleComponent oracleComponent;

        public ReviewTableService(IOracleComponent oracleComponent)
        {
            this.oracleComponent = oracleComponent;
        }

        public BaseListModel<Review> GetPart(int startRow = 1)
        {
            var connection = oracleComponent.GetOpenConnection();
            var baseListModel = new BaseListModel<Review>
            {
                Entities = GetList(oracleComponent.CommandForGetPart(connection, "Reviews", startRow)),
                EntitiesCount = oracleComponent.GetRowsCount(connection, "Reviews")
            };

            connection.Dispose();
            return baseListModel;
        }

        public void Create(Review model)
        {
            var connection = oracleComponent.GetOpenConnection();
            var command = oracleComponent.GetCommand(connection, "AddReview", CommandType.StoredProcedure);
            oracleComponent.AddParameter(command, "ProductIdVar", OracleDbType.Int32, model.ProductId);
            oracleComponent.AddParameter(command, "UserIdVar", OracleDbType.Int32, model.UserId);
            oracleComponent.AddParameter(command, "RatingVar", OracleDbType.Int32, model.Rating);
            oracleComponent.AddParameter(command, "CommentsVar", OracleDbType.Varchar2, model.Comments);
            command.ExecuteNonQuery();
            command.Dispose();
            connection.Close();
        }

        public void Delete(int id)
        {
            var connection = oracleComponent.GetOpenConnection();
            var command = oracleComponent.GetCommand(connection, "DeleteReview", CommandType.StoredProcedure);
            oracleComponent.AddParameter(command, "IdVar", OracleDbType.Int32, id);
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void Update(Review model)
        {
            var connection = oracleComponent.GetOpenConnection();
            var command = oracleComponent.GetCommand(connection, "UpdateReview", CommandType.StoredProcedure);
            oracleComponent.AddParameter(command, "IdVar", OracleDbType.Int32, model.ProductId);
            oracleComponent.AddParameter(command, "ProductIdVar", OracleDbType.Int32, model.ProductId);
            oracleComponent.AddParameter(command, "UserIdVar", OracleDbType.Int32, model.UserId);
            oracleComponent.AddParameter(command, "RatingVar", OracleDbType.Int32, model.Rating);
            oracleComponent.AddParameter(command, "CommentsVar", OracleDbType.Varchar2, model.Comments);
            command.ExecuteNonQuery();
            command.Dispose();
            connection.Close();
        }

        public Review GetById(int id)
        {
            var connection = oracleComponent.GetOpenConnection();
            var command = oracleComponent.GetCommand(connection, "select * from reviews where id = :idVar", CommandType.Text);
            oracleComponent.AddParameter(command, "idVar", OracleDbType.Int32, id);
            OracleDataReader dataReader = command.ExecuteReader();
            var review = new Review();
            while (dataReader.Read())
            {
                review.Id = dataReader.GetInt32(0);
                review.ProductId = dataReader.GetInt32(1);
                review.UserId = dataReader.GetInt32(2);
                review.Rating = dataReader.GetInt32(3);
                review.Comments = dataReader.GetString(4);
            }

            dataReader.Close();
            connection.Dispose();
            return review;
        }

        public Review GetEmpty()
        {
            return new Review();
        }

        private List<Review> GetList(OracleCommand command)
        {
            OracleDataReader dataReader = command.ExecuteReader();
            var reviewList = new List<Review>();
            while (dataReader.Read())
            {
                reviewList.Add(new Review
                {
                    Id = dataReader.GetInt32(0),
                    ProductId = dataReader.GetInt32(1),
                    UserId = dataReader.GetInt32(2),
                    Rating = dataReader.GetInt32(3),
                    Comments = dataReader.GetString(4),
                    RowNum = dataReader.GetInt32(5)
                });
            }

            return reviewList.OrderBy(x => x.Id).ToList();
        }
    }
}