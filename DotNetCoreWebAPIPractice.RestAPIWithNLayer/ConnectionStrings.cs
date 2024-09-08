using System.Data.SqlClient;

namespace DotNetCoreWebAPIPractice.RestAPIWithNLayer
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

