namespace Timweb.Api.Services.Sql
{
    /// <summary>
    ///     Brands-related SQL queries
    /// </summary>
    public static class BrandSql
    {
        /// <summary>
        ///     SQL query for inserting a brand
        /// </summary>
        public const string Insert = @"INSERT INTO brands (logo, name_en, name_ru) VALUES (@Logo, @NameEn, @NameRu) RETURNING id";

        /// <summary>
        ///     SQL query for updating a brand
        /// </summary>
        public const string Update = @"UPDATE brands SET logo = @Logo, name_en = @NameEn, name_ru = @NameRu WHERE id = @Id";
        
        /// <summary>
        ///     SQL query for deleting a brand
        /// </summary>
        public const string Delete = @"DELETE FROM brands WHERE id = @Id";
        
        /// <summary>
        ///     SQL Query for retrieving brands
        /// </summary>
        /// <param name="limit">How many results to return</param>
        /// <param name="skip">How many results to skip</param>
        /// <returns>SQL query for retrieving brands</returns>
        public static string Select(string? limit, string? skip)
        {
            return HelpersSql.AddLimitSkip("SELECT id, logo, name_en AS NameEn, name_ru AS NameRu FROM brands", limit, skip);
        }
    }
}