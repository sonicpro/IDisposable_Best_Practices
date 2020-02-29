using System;
using System.Configuration;
using System.Data.SqlClient;

namespace GetDate.ConsoleApp
{
    public class DatabaseState : IDisposable
    {
        protected SqlConnection _connection;

        protected bool _disposed;

        public virtual string GetDate()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException("DatabaseState");
            }

            if (_connection == null)
            {
                var connectionString = ConfigurationManager.ConnectionStrings["master"];
                _connection = new SqlConnection(connectionString.ConnectionString);
                _connection.Open();
            }
            using (var cmd = _connection.CreateCommand())
            {
                cmd.CommandText = "SELECT getdate()";
                return cmd.ExecuteScalar().ToString();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing && // "true" means that it's the client code calls the public Dispose() method of our class. GC never calls Dispose().
                _connection != null)
            {
                _connection.Dispose();
                _connection = null;
                _disposed = true;
            }
        }
    }
}
