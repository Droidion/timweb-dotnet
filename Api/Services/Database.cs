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
        ///     Retrieves data from the database without table relations
        /// </summary>
        /// <param name="query">SQL query</param>
        /// <typeparam name="T">Data structure to map the query result to</typeparam>
        /// <exception cref="InvalidOperationException">Could not make a DB query</exception>
        /// <returns>Result of the request mapped to a C# type</returns>
        Task<List<T>> QueryDb<T>(string query);
        
        /// <summary>
        ///     Retrieves data from the database with a single table relation
        /// </summary>
        /// <param name="query">SQL query</param>
        /// <param name="connector">Function for making a relation between tables</param>
        /// <param name="splitOn">Column name for splitting types</param>
        /// <typeparam name="TFirst">First input type</typeparam>
        /// <typeparam name="TSecond">Second input type</typeparam>
        /// <typeparam name="TReturn">Returned type</typeparam>
        /// <returns>Result of the request mapped to a C# types</returns>
        Task<List<TReturn>> QueryDb<TFirst, TSecond, TReturn>(string query, Func<TFirst, TSecond, TReturn> connector, string splitOn);

        /// <summary>
        ///     Inserts DTO to a Postgres database
        /// </summary>
        /// <param name="query">SQL query</param>
        /// <param name="dto">DTO to insert</param>
        /// <typeparam name="T">DTO type</typeparam>
        /// <returns>Id of the inserted object wrapped into a type</returns>
        /// <exception cref="InvalidOperationException">Could not make a DB insert</exception>
        Task<int> InsertDb<T>(string query, T dto);

        /// <summary>
        ///     Update DTO in a Postgres database
        /// </summary>
        /// <param name="query">SQL query</param>
        /// <param name="dto">DTO to update</param>
        /// <typeparam name="T">DTO type</typeparam>
        /// <returns>Number of rows affected</returns>
        /// <exception cref="InvalidOperationException">Could not execute a DB operation</exception>
        Task<int> ExecuteDb<T>(string query, T dto);
    }

    /// <inheritdoc />
    public class Database : IDatabase
    {
        /// <summary>
        ///     Postgres database connection string
        /// </summary>
        private readonly string _connectionString;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Database" /> class.
        /// </summary>
        /// <param name="configuration">Dependency injected app configuration</param>
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
                return (await db.QueryAsync<T>(query)).ToList();
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("Could not make a DB query", e);
            }
        }
        
        /// <inheritdoc />
        public async Task<List<TReturn>> QueryDb<TFirst, TSecond, TReturn>(string query, Func<TFirst, TSecond, TReturn> connector, string splitOn)
        {
            try
            {
                await using var db = CreateConnection();
                return (await db.QueryAsync(query, connector, splitOn: splitOn)).ToList();
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("Could not make a DB query", e);
            }
        }

        /// <inheritdoc />
        public async Task<int> InsertDb<T>(string query, T dto)
        {
            try
            {
                await using var db = CreateConnection();
                return db.QuerySingleAsync(query, dto).Id;
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("Could not make a DB insert", e);
            }
        }
        
        /// <inheritdoc />
        public async Task<int> ExecuteDb<T>(string query, T dto)
        {
            try
            {
                await using var db = CreateConnection();
                return await db.ExecuteAsync(query, dto);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("Could not execute a DB operation", e);
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
}