namespace Timweb.Api.Services
{
    /// <summary>
    /// SQL Queries to be used with Dapper
    /// </summary>
    public static class Sql
    {
        /// <summary>
        /// List of all brands
        /// </summary>
        public const string SelectBrands = "SELECT id, logo, name_en AS NameEn, name_ru AS NameRu FROM brands;";
    }
}