using SalesDatePrediction.Infrastucture;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace SalesDatePrediction.Repositories
{
    public abstract class DbRepository
    {
        private readonly DbConnectionManager _dbConnectionManager;
        public DbRepository()
        {
            _dbConnectionManager = new DbConnectionManager();
        }

        protected async Task<List<T>> ExecuteQueryAsync<T>(string query, Func<SqlDataReader, T> mapResult)
        {
            var resultList = new List<T>();

            using (var connection = _dbConnectionManager.GetConnection())
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand(query, connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            resultList.Add(mapResult(reader));
                        }
                    }
                }
            }

            return resultList;
        }

        protected List<T> ExecuteStoredProcedure<T>(string procedureName, Func<SqlDataReader, T> mapFunction, params SqlParameter[] parameters)
        {
            var results = new List<T>();

            using (var connection = _dbConnectionManager.GetConnection())
            {
                connection.Open();

                using (var command = new SqlCommand(procedureName, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            results.Add(mapFunction(reader));
                        }
                    }
                }
            }

            return results;
        }
    }
}