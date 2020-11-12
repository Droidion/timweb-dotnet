using System;

namespace Timweb.Models
{
    public record Brand
    {
        public int Id { get; }
        public string Logo { get; set; }
        public string NameRu { get; set; }
        public string NameEn { get; set; }
    }
}