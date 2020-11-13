using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Timweb.Api.Services
{
    /// <summary>
    ///     Operations on Postgres database
    /// </summary>
    public interface IDatabase
    {
        /// <summary>
        ///     Retrieves data from the database
        /// </summary>
        /// <param name="query">SQL query</param>
        /// <typeparam name="T">Data structure to map the query result to</typeparam>
        /// <returns>Result of the request mapped to a C# class</returns>
        Task<List<T>> QueryDb<T>(string query);
    }

    /// <inheritdoc />
    public class Database : IDatabase
    {
        /// <summary>
        ///     Postgres database connection string
        /// </summary>
        private readonly string _connectionString;

        public Database(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Postgres");
        }

        /// <inheritdoc />
        public async Task<List<T>> QueryDb<T>(string query)
        {
            await using var db = CreateConnection();
            return db.Query<T>(query).ToList();
        }

        /// <summary>
        ///     Creates new Postgres database connection
        /// </summary>
        /// <returns>Postgres database connection</returns>
        private NpgsqlConnection CreateConnection()
        {
            return new NpgsqlConnection(_connectionString);
        }
    }
}