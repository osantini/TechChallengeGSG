using Microsoft.Data.SqlClient;

namespace TechChallengeGSG.Service
{
    public class AzureConnection
    {
        public static SqlConnection OpenConnectionSql()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            string conn = configuration.GetConnectionString("AzureDBConnection");

            SqlConnection connection = new SqlConnection(conn);

            return connection;
        }

        public static string OpenConnectionStorage()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            string conn = configuration.GetConnectionString("StorageConnection"); 

            return conn;
        }

    }
}
