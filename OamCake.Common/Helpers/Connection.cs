using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace OamCake.Common.Helpers
{
    public interface IConnection
    {
        string ConnectionString { get; set; }
        SqlConnection GetConnection();
    }

    public class Connection : IConnection
    {
        public string ConnectionString { get; set; }

        public Connection(IConfiguration configuration)
        {
            ConnectionString = configuration.GetConnectionString("Default");
        }

        public SqlConnection GetConnection()
        {
            return new SqlConnection(ConnectionString);
        }
    }
}
