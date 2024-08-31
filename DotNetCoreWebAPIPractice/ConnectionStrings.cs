using System.Data.SqlClient;

namespace DotNetCoreWebAPIPractice
{
    public class ConnectionStrings
    {
        public static SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
        {
            DataSource = "localhost",
            InitialCatalog = "DotNetPractice",
            UserID = "myserver",
            Password = "password",
            TrustServerCertificate = true,
        };
    }
}

