namespace Timweb.Api.Services.Sql
{
    /// <summary>
    ///     Client-related SQL queries
    /// </summary>
    public static class ClientSql
    {
        /// <summary>
        ///     SQL Query for retrieving clients
        /// </summary>
        /// <param name="limit">How many results to return</param>
        /// <param name="skip">How many results to skip</param>
        /// <returns>SQL query for retrieving clients</returns>
        public static string Select(string? limit, string? skip)
        {
            return HelpersSql.AddLimitSkip(
                @"SELECT c.id, c.name_en AS NameEn, c.name_ru AS NameRu, b.logo, b.id, b.name_en AS NameEn, b.name_ru AS NameRu 
                FROM clients AS c 
                INNER JOIN brands AS b ON b.id = c.brand_id", limit, skip);
        }
    }
}