namespace Timweb.Api.Services
{
    /// <summary>
    ///     SQL Queries to be used with Dapper
    /// </summary>
    public static class Sql
    {
        /// <summary>
        /// SQL Query for retrieving brands
        /// </summary>
        /// <param name="limit">How many results to return</param>
        /// <param name="skip">How many results to skip</param>
        /// <returns>SQL Query for retrieving brands</returns>
        public static string SelectBrands(string? limit, string? skip)
        {
            return AddLimitSkip("SELECT id, logo, name_en AS NameEn, name_ru AS NameRu FROM brands", limit, skip);
        }

        /// <summary>
        /// Adds LIMIT and OFFSET clauses to the SQL query
        /// </summary>
        /// <param name="query">SQL query to add clauses to</param>
        /// <param name="limit">How many results to return</param>
        /// <param name="skip">How many results to skip</param>
        /// <returns>SQL query with added clauses</returns>
        public static string AddLimitSkip(string query, string? limit, string? skip)
        {
            if (int.TryParse(limit, out var lim)) query += $" LIMIT {lim}";
            if (int.TryParse(skip, out var sk)) query += $" OFFSET {sk}";
            return query;
        }
        
    }
}