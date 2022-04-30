using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using RazorPageExample.Utilities;
using System.Data.SqlClient;

namespace RazorPageExample.DataAccess.DataAccess
{
    public class SqlDataAccess : ISqlDataAccess
    {

        public string connectionString = ConfigurationManager.AppSettings["ConnectionStrings:EmployeeConnectionString"];

        public DataTable GetDataTableWithoutParameters(string sqlQuery)
        {
            try
            {


                DataTable dt = new DataTable();
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string sql = sqlQuery;
                    SqlCommand command = new SqlCommand(sql, connection);

                    SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                    dataAdapter.Fill(dt);

                    connection.Close();
                }

                return dt;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
        }
    }
}
