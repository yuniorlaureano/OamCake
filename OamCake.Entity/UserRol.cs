namespace OamCake.Entity
{
    public class UserRol
    {
        public int Id { get; set; }
        public byte RoleId { get; set; }
        public int UserId { get; set; }
        public DateTime AssignedDate { get; set; }

        public User User { get; set; }
    }
}
