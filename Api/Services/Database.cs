using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using Timweb.Models;

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

        /// <summary>
        ///     Inserts DTO to a Postgres database
        /// </summary>
        /// <param name="query">SQL query</param>
        /// <param name="dto">DTO to insert</param>
        /// <typeparam name="T">DTO type</typeparam>
        /// <returns>Id of the inserted object wrapped into a type</returns>
        /// <exception cref="InvalidOperationException">Could not make a DB insert</exception>
        Task<int> InsertDb<T>(string query, T dto);
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
                return db.Query<T>(query).ToList();
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
                return db.QuerySingle(query, dto).Id;
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
}