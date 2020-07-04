using System;
using System.Data;
using System.Data.SqlClient;

namespace Factory
{
    static class ConnectionFactory
    {
        public static IDbConnection GetConnection(string strConnection)
        {
            var connection = new SqlConnection(strConnection);
            connection.Open();
            return connection;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Factory Get SQL Connection");
            IDbConnection connection = ConnectionFactory.GetConnection("connection string");
        }
    }
}
