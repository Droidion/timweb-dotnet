namespace Timweb.Models
{
    public class City
    {
        public Country Country { get; set; }
        public Name Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}