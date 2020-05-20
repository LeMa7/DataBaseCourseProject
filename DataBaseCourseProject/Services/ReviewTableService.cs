﻿using DataBaseCourseProject.ComponentInterfaces;
using DataBaseCourseProject.Models;
using DataBaseCourseProject.ServiceInterfaces;
using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;
using System.Data;

namespace DataBaseCourseProject.Services
{
    public class ReviewTableService : ITableService<Review>
    {
        private readonly IOracleComponent oracleComponent;

        public ReviewTableService(IOracleComponent oracleComponent)
        {
            this.oracleComponent = oracleComponent;
        }

        public List<Review> GetAll()
        {
            var connection = oracleComponent.GetOpenConnection();
            var command = oracleComponent.GetCommand(connection, "select * from reviews", CommandType.Text);
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
                    Comments = dataReader.GetString(4)
                });
            }

            dataReader.Close();
            connection.Dispose();
            return reviewList;
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

        public Review GetEmpty()
        {
            return new Review();
        }
    }
}