using System.Data;
using Microsoft.Data.SqlClient;

namespace MyWebApiApp.Utilities
{
    public class DBHelper
    {
        private readonly string _connectionString;

        public DBHelper(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConnectionString");
        }

        // create and open sql connection
        public SqlConnection GetConnection()
        {
            var conn = new SqlConnection(_connectionString);
            conn.Open();
            return conn;
        }

        // create cmd for given conn and stored procedure and add parameter list
        public SqlCommand CreateCommand(SqlConnection conn, string storedProcedureName, params SqlParameter[] parameters)
        {
            var cmd = new SqlCommand(storedProcedureName, conn)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            };

            if (parameters != null)
            {
                cmd.Parameters.AddRange(parameters);
            }

            return cmd;
        }

        public DataTable ExecuteDataTable(string storedProcedureName, params SqlParameter[] parameters)
        {
            using var conn = GetConnection();
            using var cmd = CreateCommand(conn, storedProcedureName, parameters);
            using var adapter = new SqlDataAdapter(cmd);
            var dt = new DataTable();
            adapter.Fill(dt);
            return dt;
        }

        public Object ExecuteScalar(string storedProcedureName, params SqlParameter[] parameters)
        {
            using var conn = GetConnection();
            using var cmd = CreateCommand(conn, storedProcedureName, parameters);
            return cmd.ExecuteScalar();
        }

        public int ExecuteNonQuery(string storedProcedureName, params SqlParameter[] parameters)
        {
            using var conn = GetConnection();
            using var cmd = CreateCommand(conn, storedProcedureName, parameters);
            return cmd.ExecuteNonQuery();
        }
    }
}