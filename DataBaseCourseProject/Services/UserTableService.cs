using DataBaseCourseProject.ComponentInterfaces;
using DataBaseCourseProject.Models.Tables;
using DataBaseCourseProject.ServiceInterfaces;
using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;
using System.Data;

namespace DataBaseCourseProject.Services
{
    public class UserTableService : ITableService<User>
    {
        private readonly IOracleComponent oracleComponent;

        public UserTableService(IOracleComponent oracleComponent)
        {
            this.oracleComponent = oracleComponent;
        }

        public List<User> GetAll()
        {
            var connection = oracleComponent.GetOpenConnection();
            var command = oracleComponent.GetCommand(connection, "select * from users", CommandType.Text);
            OracleDataReader dataReader = command.ExecuteReader();
            var userList = new List<User>();
            while (dataReader.Read())
            {
                userList.Add(new User
                {
                    Id = dataReader.GetInt32(0),
                    FirstName = dataReader.GetString(1),
                    MiddleName = dataReader.GetString(2),
                    LastName = dataReader.GetString(3),
                    Email = dataReader.GetString(4),
                    PhoneNumber = dataReader.GetString(5)
                });
            }

            dataReader.Close();
            connection.Dispose();
            return userList;
        }

        public void Create(User model)
        {
            var connection = oracleComponent.GetOpenConnection();
            var command = oracleComponent.GetCommand(connection, "AddUser", CommandType.StoredProcedure);
            oracleComponent.AddParameter(command, "FirstNameVar", OracleDbType.Varchar2, model.FirstName);
            oracleComponent.AddParameter(command, "MiddleNameVar", OracleDbType.Varchar2, model.MiddleName);
            oracleComponent.AddParameter(command, "LastNameVar", OracleDbType.Varchar2, model.LastName);
            oracleComponent.AddParameter(command, "EmailVar", OracleDbType.Varchar2, model.Email);
            oracleComponent.AddParameter(command, "PhoneNumberVar", OracleDbType.Varchar2, model.PhoneNumber);
            command.ExecuteNonQuery();
            command.Dispose();
            connection.Close();
        }

        public void Delete(int id)
        {
            var connection = oracleComponent.GetOpenConnection();
            var command = oracleComponent.GetCommand(connection, "DeleteUser", CommandType.StoredProcedure);
            oracleComponent.AddParameter(command, "IdVar", OracleDbType.Int32, id);
            command.ExecuteNonQuery();
            connection.Close();
        }

        public User GetEmpty()
        {
            return new User();
        }
    }
}