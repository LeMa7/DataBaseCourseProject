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

        public OracleCommand CommandForGetPart(OracleConnection connection, string table, int startRow)
        {
            string select = string.Format(
                "SELECT * FROM(SELECT t.*, ROWNUM AS rowNumber FROM(SELECT * FROM {0} ORDER BY Id) t) WHERE rowNumber BETWEEN: startRow AND: endRow",
                table);
            var command = GetCommand(
                connection, select, CommandType.Text);
            AddParameter(command, "startRow", OracleDbType.Varchar2, (startRow - 1) * 150 + 1);
            AddParameter(command, "endRow", OracleDbType.Varchar2, startRow * 150);
            return command;
        }

        public int GetRowsCount(OracleConnection connection, string table)
        {
            var command = GetCommand(connection, string.Format("SELECT COUNT(*) FROM {0}", table), CommandType.Text);
            OracleDataReader dataReader = command.ExecuteReader();
            var count = 0;
            while (dataReader.Read())
            {
                count = dataReader.GetInt32(0);
            }

            return count;
        }
    }
}