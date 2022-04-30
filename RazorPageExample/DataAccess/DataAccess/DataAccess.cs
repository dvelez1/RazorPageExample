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

        public async Task<Tuple<DataTable, int>> GetDataTableWithoutParametersAsync(string sqlQuery)
        {
            DataTable dt = new DataTable();
            int sqlTransactionResult = 0;
            try
            {
                await using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await  connection.OpenAsync();

                    string sql = sqlQuery;
                    SqlCommand command = new SqlCommand(sql, connection);

                    SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                    dataAdapter.Fill(dt);

                    await connection.CloseAsync();
                }

               
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new Tuple<DataTable, int>(dt, -1);
            }
            return new Tuple<DataTable, int>(dt, sqlTransactionResult);
        }

        public async Task<Tuple<DataTable, int>> GetDataTableWithParametersAsync(string sqlQuery, List<(string parameterName, object value)> parameters)
        {
            int sqlTransactionResult = 0;
            DataTable dt = new DataTable();
            try
            {
                await using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    string sql = sqlQuery;
                    SqlCommand command = new SqlCommand(sql, connection);
                    foreach (var parameter in parameters)
                    {
                        if (parameter.value == null)
                            command.Parameters.AddWithValue(parameter.parameterName, System.DBNull.Value);
                        else
                            command.Parameters.AddWithValue(parameter.parameterName, parameter.value);
                    }

                    // Used to return SqlTransaction Flag (good or fails)
                    //var returnParameter = command.Parameters.Add("@ReturnSqlParameter", SqlDbType.Int);
                    //returnParameter.Direction = ParameterDirection.ReturnValue;

                    SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                    dataAdapter.Fill(dt);

                    await connection.CloseAsync();

                    // Used to return SqlTransaction Flag (good or fails)
                    //sqlTransactionResult = (int)command.Parameters["@ReturnSqlParameter"].Value;
                }

                return new Tuple<DataTable, int>(dt, sqlTransactionResult);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new Tuple<DataTable, int>(new DataTable(), -1);
            }

        }

        public async Task<int> UpsertSqlDatabaseAsync(string sqlQuery, List<(string parameterName, object value)> parameters)
        {
            int sqlTransactionResult = 0;
            try
            {
                await using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    string sql = sqlQuery;
                    SqlCommand command = new SqlCommand(sql, connection);
                    foreach (var parameter in parameters)
                    {
                        if (parameter.value == null)
                            command.Parameters.AddWithValue(parameter.parameterName, System.DBNull.Value);
                        else
                            command.Parameters.AddWithValue(parameter.parameterName, parameter.value);
                    }

                    // Used to return SqlTransaction Flag (good or fails)
                    //var returnParameter = command.Parameters.Add("@ReturnSqlParameter", SqlDbType.Int);
                    //returnParameter.Direction = ParameterDirection.ReturnValue;

                    await command.ExecuteNonQueryAsync();

                    await connection.CloseAsync();

                    //Used to return SqlTransaction Flag(good or fails)
                    //sqlTransactionResult = (int)command.Parameters["@ReturnSqlParameter"].Value;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return -1;
            }
            return sqlTransactionResult;
        }
    }
}
