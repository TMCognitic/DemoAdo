using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace Tools.Database
{
    public class Connection
    {
        private readonly string _connectionString;
        private DbProviderFactory _factory;

        public Connection(string connectionString, DbProviderFactory factory)
        {
            _connectionString = connectionString;
            _factory = factory;

            using(DbConnection sqlConnection = CreateConnection())
            {
                sqlConnection.Open();
            }
        }

        public int ExecuteNonQuery(Command command)
        {
            using (DbConnection sqlConnection = CreateConnection())
            {
                using (DbCommand sqlCommand = CreateCommand(command, sqlConnection))
                {
                    sqlConnection.Open();
                    return sqlCommand.ExecuteNonQuery();
                }
            }
        }        

        public object ExecuteScalar(Command command)
        {
            using (DbConnection sqlConnection = CreateConnection())
            {
                using (DbCommand sqlCommand = CreateCommand(command, sqlConnection))
                {
                    sqlConnection.Open();
                    object o = sqlCommand.ExecuteScalar();
                    return (o is DBNull) ? null : o;
                }
            }
        }

        public IEnumerable<TResult> ExecuteReader<TResult>(Command command, Func<IDataRecord, TResult> selector)
        {
            if (selector is null)
                throw new ArgumentNullException(nameof(selector));

            using (DbConnection sqlConnection = CreateConnection())
            {
                using (DbCommand sqlCommand = CreateCommand(command, sqlConnection))
                {
                    sqlConnection.Open();
                    using (IDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        while(dataReader.Read())
                        {
                            yield return selector(dataReader);
                        }
                    }
                }
            }
        }

        public DataTable GetDataTable(Command command)
        {
            using (DbConnection sqlConnection = CreateConnection())
            {
                using (DbCommand sqlCommand = CreateCommand(command, sqlConnection))
                {
                    using(DbDataAdapter sqlDataAdapter = _factory.CreateDataAdapter())
                    {
                        sqlDataAdapter.SelectCommand = sqlCommand;
                        DataTable dataTable = new DataTable();
                        sqlDataAdapter.Fill(dataTable);
                        return dataTable;
                    }
                }
            }
        }

        public DataSet GetDataSet(Command command)
        {
            using (DbConnection sqlConnection = CreateConnection())
            {
                using (DbCommand sqlCommand = CreateCommand(command, sqlConnection))
                {
                    using (DbDataAdapter sqlDataAdapter = _factory.CreateDataAdapter())
                    {
                        sqlDataAdapter.SelectCommand = sqlCommand;
                        DataSet dataSet = new DataSet();
                        sqlDataAdapter.Fill(dataSet);
                        return dataSet;
                    }
                }
            }
        }

        private DbCommand CreateCommand(Command command, DbConnection sqlConnection)
        {
            DbCommand sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandText = command.Query;
            
            if (command.IsStoredProcedure)
                sqlCommand.CommandType = CommandType.StoredProcedure;

            foreach(KeyValuePair<string, Parameter> kvp in command.Parameters)
            {
                DbParameter sqlParameter = _factory.CreateParameter();
                sqlParameter.ParameterName = kvp.Key;
                sqlParameter.Value = kvp.Value.Value;
                sqlParameter.Direction = kvp.Value.Direction;

                sqlCommand.Parameters.Add(sqlParameter);
            }

            return sqlCommand;
        }

        private DbConnection CreateConnection()
        {
            DbConnection sqlConnection = _factory.CreateConnection();
            sqlConnection.ConnectionString = _connectionString;

            return sqlConnection;
        }
    }
}
