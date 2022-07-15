namespace OamCake.Entity
{
    public class MenuRolPermit : BaseEntity
    {
        public long Id { get; set; }
        public long MenuPermitId { get; set; }
        public long RoleId { get; set; }

        public MenuPermit MenuPermit { get; set; }
        public Role Role { get; set; }
    }
}


