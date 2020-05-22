using DataBaseCourseProject.ComponentInterfaces;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace DataBaseCourseProject.Components
{
    public class OracleComponent : IOracleComponent
    {
        private const string connectionString = "User Id=system;Password=qwerty;Data Source=localhost:1521/orcl";

        public OracleConnection GetOpenConnection()
        {
            var connection = new OracleConnection(connectionString);
            connection.Open();
            return connection;
        }

        public OracleCommand GetCommand(OracleConnection connection, string commandText, CommandType commandType)
        {
            return new OracleCommand
            {
                Connection = connection,
                CommandText = commandText,
                CommandType = commandType
            };
        }

        public void AddParameter(OracleCommand command, string name, OracleDbType type, object value)
        {
            command.Parameters.Add(new OracleParameter(name, type)
            {
                Value = value
            });
        }
    }
}