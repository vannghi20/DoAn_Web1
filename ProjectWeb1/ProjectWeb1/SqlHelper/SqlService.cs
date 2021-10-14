using Microsoft.Extensions.Options;
using ProjectWeb1.Common.Config;
using ProjectWeb1.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectWeb1.SqlHelper
{
    public class SqlService:ISqlServer
    {
        private readonly GetConnectionString _getConnectionString;
        
        public SqlService(IOptions<GetConnectionString> getConnectionString)
        {
            _getConnectionString = getConnectionString.Value;
        }
        public async Task<DataTable> GetData(string str)
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(_getConnectionString.SqlConnection))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(str, connection))
                    {
                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            dataTable.Load(reader);
                            reader.Close();
                            connection.Close();
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }
            return dataTable;
        }
        public async Task<int> ExcuteDate(string str, params IDataParameter[] sqlParams)
        {
            int rows = -1;
            try
            {
                using (SqlConnection connection = new SqlConnection(_getConnectionString.SqlConnection))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(str, connection))
                    {
                        if (sqlParams != null)
                        {
                            foreach (IDataParameter parameter in sqlParams)
                            {
                                command.Parameters.Add(parameter);

                            }
                            rows = await command.ExecuteNonQueryAsync();

                        }
                    }
                    connection.Close();
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }
            return rows;
        }
    }
}
