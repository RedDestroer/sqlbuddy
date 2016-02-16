using System.Data;
using System.Data.SqlClient;

namespace SqlBuddy.Dal
{
    public interface IExecutionContext
    {
        void Open();
        void Close();
        int ExecuteNonQuery(SqlCommand command);
        IDataReader ExecuteReader(SqlCommand command);
    }
}