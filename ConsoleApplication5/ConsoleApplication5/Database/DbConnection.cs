using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Configuration;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using System.Data;

namespace FlowersGirl.Database
{
    public class DbConnection
    {
        private readonly static string ConnectionString = ConfigurationManager.AppSettings["connectionStr"];

        private R ConnectToDbAndExecute<R>(Func<SqlConnection, R> function)
        {
            using (var cn = new SqlConnection(ConnectionString))
            {
                cn.Open();
                return function(cn);
            }
        }

        public DataTable ExecuteResultOperation(string query, SqlParameter[] parameters)
        {
            Func<SqlConnection, DataTable> func = connection =>
            {
                DbCommand dbCommand = new SqlCommand(query, connection);

                dbCommand.Parameters.AddRange(parameters);

                DataTable result = new DataTable();

                using (var dbReader = dbCommand.ExecuteReader())
                {
                    result.Load(dbReader);
                }

                return result;
            };

            return ConnectToDbAndExecute(func);
        }

        public int ExecuteResultIdOperation(string query, SqlParameter[] parameters)
        {
            Func<SqlConnection, int> func = connection =>
            {
                DbCommand dbCommand = new SqlCommand(query, connection);

                dbCommand.Parameters.AddRange(parameters);

                return (int)dbCommand.ExecuteScalar();
            };

            return ConnectToDbAndExecute(func);
        }

        public void ExecuteNonResultOperation(string query, SqlParameter[] parameters)
        {
            Func<SqlConnection, object> function = connection =>
            {
                DbCommand dbCommand = new SqlCommand(query, connection);

                dbCommand.Parameters.AddRange(parameters);

                dbCommand.ExecuteNonQuery();

                return null;
            };

            ConnectToDbAndExecute(function);
        }

        public DataTable ExecuteProcedure(string procedureName, SqlParameter[] parameters)
        {
            Func<SqlConnection, DataTable> func = connection =>
            {
                DbCommand dbCommand = new SqlCommand(procedureName, connection);

                dbCommand.Parameters.AddRange(parameters);
                dbCommand.CommandType = CommandType.StoredProcedure;

                DataTable result = new DataTable();

                using (var dbReader = dbCommand.ExecuteReader())
                {
                    result.Load(dbReader);
                }

                return result;
            };

            return ConnectToDbAndExecute(func);
        }
    }
}
