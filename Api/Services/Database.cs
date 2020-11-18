using System;
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
        /// <exception cref="InvalidOperationException">Could not make a DB query</exception>
        /// <returns>Result of the request mapped to a C# class</returns>
        Task<List<T>> QueryDb<T>(string query);

        Task<IdContainer> InsertDb<T>(T item, string query);
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
            try
            {
                await using var db = CreateConnection();
                return db.Query<T>(query).ToList();
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("Could not make a DB query", e);
            }
        }

        public async Task<IdContainer> InsertDb<T>(T item, string query)
        {
            try
            {
                await using var db = CreateConnection();
                return db.QuerySingle<IdContainer>(query, item);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("Could not make a DB insert", e);
            }
        }

        /// <summary>
        ///     Creates new Postgres database connection
        /// </summary>
        /// <returns>Postgres database connection</returns>
        private NpgsqlConnection CreateConnection()
        {
            return new(_connectionString);
        }
        
    }

    public class IdContainer
    {
        public int Id { get; set; }
    }
}