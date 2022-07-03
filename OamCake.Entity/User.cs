namespace OamCake.Entity
{
    public class User
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public string Email { get; set; }
        public char Status { get; set; }

        public Employee Employee { get; set; }
    }
}
