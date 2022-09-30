using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace CustomORM
{
    internal class QueryExecuter
    {
        public static DataSet ExecuteSelectQuery(string query)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var adapter = new SqlDataAdapter(query, connection);
                var ds = new DataSet();
                adapter.Fill(ds);
                return ds;
            }
        }
        public static void ExecuteSaveChanges()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    var cmd = new SqlCommand();
                    cmd.Transaction = transaction;
                    try
                    {
                        cmd.CommandText = String.Join(";", ListOfQuery.Queries);
                        cmd.Connection = connection;
                        cmd.ExecuteNonQuery();
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                    }
                }
            }
        }
    }
}
