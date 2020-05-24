using DataBaseCourseProject.ComponentInterfaces;
using DataBaseCourseProject.ServiceInterfaces;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace DataBaseCourseProject.Services
{
    public class ActionService : IActionService
    {
        private readonly IOracleComponent oracleComponent;

        public ActionService(IOracleComponent oracleComponent)
        {
            this.oracleComponent = oracleComponent;
        }

        public string GetDiff(int? firstId, int? secondId)
        {
            var connection = oracleComponent.GetOpenConnection();
            var command = oracleComponent.GetCommand(connection, "Comparison", CommandType.StoredProcedure);
            oracleComponent.AddParameter(command, "FirstId", OracleDbType.Int32, firstId);
            oracleComponent.AddParameter(command, "SecondId", OracleDbType.Int32, secondId);
            oracleComponent.AddOutParameter(command, "Differences", OracleDbType.Varchar2, "");
            command.ExecuteNonQuery();
            return command.Parameters["Differences"]?.Value?.ToString();
        }
    }
}