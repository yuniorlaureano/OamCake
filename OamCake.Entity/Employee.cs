namespace OamCake.Entity
{
    public class Employee : BaseEntity
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string? Photo { get; set; }
    }
}
