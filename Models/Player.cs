namespace Timweb.Models
{
    public class Player
    {
        public string FullName { get; set; }
        public TeamRole[] Roles { get; set; }
        public Team Team { get; set; }
    }
}