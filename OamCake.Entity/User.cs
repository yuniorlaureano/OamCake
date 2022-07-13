namespace OamCake.Entity
{
    public class User : BaseEntity
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }

        public Employee Employee { get; set; }
    }
}
