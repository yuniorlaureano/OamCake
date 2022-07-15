namespace OamCake.Entity
{
    public class UserRol : BaseEntity
    {
        public long Id { get; set; }
        public long RoleId { get; set; }
        public long UserId { get; set; }
        public DateTime AssignedDate { get; set; }

        public User User { get; set; }
    }
}
