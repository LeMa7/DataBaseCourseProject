using DataBaseCourseProject.ComponentInterfaces;
using DataBaseCourseProject.Models.Tables;
using DataBaseCourseProject.ServiceInterfaces;
using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;
using System.Data;

namespace DataBaseCourseProject.Services
{
    public class ProducerTableService : ITableService<Producer>
    {
        private readonly IOracleComponent oracleComponent;

        public ProducerTableService(IOracleComponent oracleComponent)
        {
            this.oracleComponent = oracleComponent;
        }

        public List<Producer> GetAll()
        {
            var connection = oracleComponent.GetOpenConnection();
            var command = oracleComponent.GetCommand(connection, "select * from producers", CommandType.Text);
            OracleDataReader dataReader = command.ExecuteReader();
            var producerList = new List<Producer>();
            while (dataReader.Read())
            {
                producerList.Add(new Producer
                {
                    Id = dataReader.GetInt32(0),
                    Name = dataReader.GetString(1)
                });
            }

            dataReader.Close();
            connection.Dispose();
            return producerList;
        }

        public void Create(Producer model)
        {
            var connection = oracleComponent.GetOpenConnection();
            var command = oracleComponent.GetCommand(connection, "AddProducer", CommandType.StoredProcedure);
            oracleComponent.AddParameter(command, "NameVar", OracleDbType.Varchar2, model.Name);
            command.ExecuteNonQuery();
            command.Dispose();
            connection.Close();
        }

        public void Delete(int id)
        {
            var connection = oracleComponent.GetOpenConnection();
            var command = oracleComponent.GetCommand(connection, "DeleteProducer", CommandType.StoredProcedure);
            oracleComponent.AddParameter(command, "IdVar", OracleDbType.Int32, id);
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void Update(Producer model)
        {
            var connection = oracleComponent.GetOpenConnection();
            var command = oracleComponent.GetCommand(connection, "UpdateProducer", CommandType.StoredProcedure);
            oracleComponent.AddParameter(command, "IdVar", OracleDbType.Int32, model.Id);
            oracleComponent.AddParameter(command, "NameVar", OracleDbType.Varchar2, model.Name);
            command.ExecuteNonQuery();
            command.Dispose();
            connection.Close();
        }

        public Producer GetById(int id)
        {
            var connection = oracleComponent.GetOpenConnection();
            var command = oracleComponent.GetCommand(connection, "select * from producers where id = :idVar", CommandType.Text);
            oracleComponent.AddParameter(command, "idVar", OracleDbType.Int32, id);
            OracleDataReader dataReader = command.ExecuteReader();
            var producer = new Producer();
            while (dataReader.Read())
            {
                producer.Id = dataReader.GetInt32(0);
                producer.Name = dataReader.GetString(1);
            }

            dataReader.Close();
            connection.Dispose();
            return producer;
        }

        public Producer GetEmpty()
        {
            return new Producer();
        }
    }
}