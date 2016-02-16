using System;
using System.Data;
using System.Data.SqlClient;

namespace SqlBuddy.Dal
{
    public class ExecutionContext
        : IExecutionContext
    {
        private readonly SqlConnection _connection;
        private readonly SqlTransaction _transaction;
        private readonly int _timeout;
        private bool _isConnectionClosed;

        public ExecutionContext(string connectionString, TimeSpan timeout)
        {
            _connection = new SqlConnection(connectionString);
            _transaction = null;
            _isConnectionClosed = true;
            _timeout = (int)timeout.TotalSeconds;
        }
        
        public void Open()
        {
            _connection.Open();
            _isConnectionClosed = false;
        }
        
        public void Close()
        {
            _connection.Close();
            _isConnectionClosed = true;
        }
        
        public int ExecuteNonQuery(SqlCommand command)
        {
            Prepare(command);

            return command.ExecuteNonQuery();
        }
        
        public IDataReader ExecuteReader(SqlCommand command)
        {
            Prepare(command);

            return command.ExecuteReader();
        }
        
        private void Prepare(SqlCommand command)
        {
            if (command == null) throw new ArgumentNullException("command");

            OpenConnectionIfClosed();
            PrepareCommand(command);
        }
        
        private void PrepareCommand(IDbCommand command)
        {
            command.Connection = _connection;
            command.Transaction = _transaction;
            command.CommandTimeout = _timeout;
        }

        private void OpenConnectionIfClosed()
        {
            if (_isConnectionClosed || _connection.State != ConnectionState.Open)
                Open();
        }
    }
}