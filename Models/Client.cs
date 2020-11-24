namespace Timweb.Models
{
    public record Client
    {
        public int? Id { get; init; }
        public string NameRu { get; init; }
        public string NameEn { get; init; }
        public Brand Brand { get; set; }
    }
}