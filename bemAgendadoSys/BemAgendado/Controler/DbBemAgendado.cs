using System;
using System.Data;
using Npgsql;

namespace BemAgendado.Controler
{
    public class DbBemAgendado : IDisposable
    {
        private readonly string _connectionString;
        private NpgsqlConnection _connection;

        public DbBemAgendado(string connectionString)
        {
            _connectionString = connectionString;
            _connection = new NpgsqlConnection(_connectionString);
        }

        public DbBemAgendado()
        {
            _connectionString = "Host=localhost;Port=5432;Database=BemAgendado;Username=postgres;Password=270805;";
            _connection = new NpgsqlConnection(_connectionString);
        }

        // ESTE MÉTODO DEVE EXISTIR!
        public NpgsqlConnection GetConnection()
        {
            return _connection;
        }

        public void Open()
        {
            if (_connection.State != ConnectionState.Open)
            {
                _connection.Open();
            }
        }

        public void Close()
        {
            if (_connection.State != ConnectionState.Closed)
            {
                _connection.Close();
            }
        }

        public void Dispose()
        {
            Close();
            _connection?.Dispose();
        }
    }
}