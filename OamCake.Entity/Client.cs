namespace OamCake.Entity
{
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public long Latitude { get; set; }
        public long Longitude { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Photo { get; set; }

        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
