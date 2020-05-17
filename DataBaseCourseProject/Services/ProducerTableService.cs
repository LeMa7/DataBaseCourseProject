﻿using DataBaseCourseProject.ComponentInterfaces;
using DataBaseCourseProject.Models;
using DataBaseCourseProject.ServiceInterfaces;
using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;
using System.Data;

namespace DataBaseCourseProject.Services
{
    public class ProducerTableService : IProducerTableService
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

        public void CreateProducer(ProducerCreateModel model)
        {
            var connection = oracleComponent.GetOpenConnection();
            var command = oracleComponent.GetCommand(connection, "AddProducer", CommandType.StoredProcedure);
            oracleComponent.AddParameter(command, "NameVar", OracleDbType.Varchar2, model.Name);
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void DeleteProducer(int id)
        {
            var connection = oracleComponent.GetOpenConnection();
            var command = oracleComponent.GetCommand(connection, "DeleteProducer", CommandType.StoredProcedure);
            oracleComponent.AddParameter(command, "IdVar", OracleDbType.Int32, id);
            command.ExecuteNonQuery();
            connection.Close();
        }
    }
}