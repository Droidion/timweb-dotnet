using System;

namespace Timweb.Models
{
    public class Event
    {
        public DateTime DateStart { get; set; }
        public DateTime DateFinish { get; set; }
        public City City { get; set; }
        public Seminar Seminar { get; set; }
        public Game? Game { get; set; }
        public Client[] Clients { get; set; }
    }
}