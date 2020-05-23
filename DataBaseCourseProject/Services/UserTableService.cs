using DataBaseCourseProject.ComponentInterfaces;
using DataBaseCourseProject.Models.Tables;
using DataBaseCourseProject.ServiceInterfaces;
using Oracle.ManagedDataAccess.Client;
using RandomNameGeneratorLibrary;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DataBaseCourseProject.Services
{
    public class UserTableService : ITableService<User>
    {
        private readonly IOracleComponent oracleComponent;

        public UserTableService(IOracleComponent oracleComponent)
        {
            this.oracleComponent = oracleComponent;
        }

        public BaseListModel<User> GetPart(int startRow = 1)
        {
            var connection = oracleComponent.GetOpenConnection();
            var baseListModel = new BaseListModel<User>
            {
                Entities = GetList(oracleComponent.CommandForGetPart(connection, "Users", startRow)),
                EntitiesCount = oracleComponent.GetRowsCount(connection, "Users")
            };

            connection.Dispose();
            return baseListModel;
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
            oracleComponent.AddParameter(command, "PasswordVar", OracleDbType.Varchar2, model.Password);
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

        public void Update(User model)
        {
            var connection = oracleComponent.GetOpenConnection();
            var command = oracleComponent.GetCommand(connection, "UpdateUser", CommandType.StoredProcedure);
            oracleComponent.AddParameter(command, "IdVar", OracleDbType.Int32, model.Id);
            oracleComponent.AddParameter(command, "FirstNameVar", OracleDbType.Varchar2, model.FirstName);
            oracleComponent.AddParameter(command, "MiddleNameVar", OracleDbType.Varchar2, model.MiddleName);
            oracleComponent.AddParameter(command, "LastNameVar", OracleDbType.Varchar2, model.LastName);
            oracleComponent.AddParameter(command, "EmailVar", OracleDbType.Varchar2, model.Email);
            oracleComponent.AddParameter(command, "PhoneNumberVar", OracleDbType.Varchar2, model.PhoneNumber);
            command.ExecuteNonQuery();
            command.Dispose();
            connection.Close();
        }

        public User GetById(int id)
        {
            var connection = oracleComponent.GetOpenConnection();
            var command = oracleComponent.GetCommand(connection, "select * from users where id = :idVar", CommandType.Text);
            oracleComponent.AddParameter(command, "idVar", OracleDbType.Int32, id);
            OracleDataReader dataReader = command.ExecuteReader();
            var user = new User();
            while (dataReader.Read())
            {
                user.Id = dataReader.GetInt32(0);
                user.FirstName = dataReader.GetString(1);
                user.MiddleName = dataReader.GetString(2);
                user.LastName = dataReader.GetString(3);
                user.Email = dataReader.GetString(4);
                user.PhoneNumber = dataReader.GetString(5);
            }

            dataReader.Close();
            connection.Dispose();
            return user;
        }

        public User GetEmpty()
        {
            return new User();
        }

        private List<User> GetList(OracleCommand command)
        {
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
                    PhoneNumber = dataReader.GetString(5),
                    Password = dataReader.GetString(6),
                    RowNum = dataReader.GetInt32(7)
                });
            }

            return userList.OrderBy(x => x.Id).ToList();
        }
    }
}