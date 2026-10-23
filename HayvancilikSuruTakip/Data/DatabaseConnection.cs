
using System.Data.SqlClient;

namespace HayvancilikSuruTakip.Data
{
    public static class DatabaseConnection
    {
        private static readonly string connectionString =
            @"Server=localhost\SQLEXPRESS21;Database=HayvancilikDB;Integrated Security=True;";

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}
