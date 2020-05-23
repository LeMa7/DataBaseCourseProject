using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace DataBaseCourseProject.ComponentInterfaces
{
    public interface IOracleComponent
    {
        OracleConnection GetOpenConnection();

        OracleCommand GetCommand(OracleConnection connection, string commandText, CommandType commandType);

        void AddParameter(OracleCommand command, string name, OracleDbType type, object value);

        OracleCommand CommandForGetPart(OracleConnection connection, string table, int startRow);

        int GetRowsCount(OracleConnection connection, string table);
    }
}