using System;
using JetBrains.Annotations;

namespace Timweb.Models
{
    /// <summary>
    ///     Brand model
    /// </summary>
    [PublicAPI]
    [Serializable]
    public record Brand
    {
        /// <summary>
        ///     Id in the database
        /// </summary>
        /// <example>100</example>
        public int? Id { get; init; }

        /// <summary>
        ///     Path to logo image
        /// </summary>
        /// <example>path/to/image.png</example>
        public string Logo { get; init; }

        /// <summary>
        ///     Brand name in Russian
        /// </summary>
        /// <example>Газпром</example>
        public string NameRu { get; init; }

        /// <summary>
        ///     Brand name in English
        /// </summary>
        /// <example>Gasprom</example>
        public string NameEn { get; init; }
    }
}