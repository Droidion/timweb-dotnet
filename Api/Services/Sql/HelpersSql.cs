namespace Timweb.Api.Services.Sql
{
    /// <summary>
    ///     Helper methods for building SQL queries
    /// </summary>
    public static class HelpersSql
    {
        /// <summary>
        ///     Adds LIMIT and OFFSET clauses to the SQL query
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