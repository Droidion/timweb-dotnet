using System;

namespace Timweb.Models
{
    public class Feedback
    {
        public Name Author { get; set; }
        public Name Text { get; set; }
        public Brand Brand { get; set; }
        public DateTime Date { get; set; }
        public string Link { get; set; }
        public string ImagePath { get; set; }
    }
}