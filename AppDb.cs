using System;
using MySql.Data.MySqlClient;


namespace RetailApp
{
    public class AppDb : IDisposable
    {
        public MySqlConnection Connection { get; }

        public AppDb(string connectionString)
        {
            Connection = new MySqlConnection(connectionString);
            Connection.Open();
        }

        public void Dispose() => Connection.Dispose();
    }
}
