using System;
using JetBrains.Annotations;

namespace Timweb.Models
{
    /// <summary>
    ///     Client model
    /// </summary>
    [PublicAPI]
    [Serializable]
    public record Client
    {
        /// <summary>
        ///     Id in the database
        /// </summary>
        /// <example>100</example>
        public int? Id { get; init; }

        /// <summary>
        ///     Client name in Russian
        /// </summary>
        /// <example>ООО Газпром</example>
        public string NameRu { get; init; }

        /// <summary>
        ///     Client name in English
        /// </summary>
        /// <example>OOO Gasprom</example>
        public string NameEn { get; init; }

        /// <summary>
        ///     Brand of the client
        /// </summary>
        public Brand Brand { get; set; }
    }
}