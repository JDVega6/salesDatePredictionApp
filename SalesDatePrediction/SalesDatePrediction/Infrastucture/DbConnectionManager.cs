using System.Configuration;
using System.Data.SqlClient;

namespace SalesDatePrediction.Infrastucture
{
    public class DbConnectionManager
    {
        private readonly string _connectionString;

        public DbConnectionManager()
        {

            _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        public SqlConnection GetConnection()
        {
            var connection = new SqlConnection(_connectionString);
            return connection;
        }
    }
}