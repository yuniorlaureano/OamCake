namespace OamCake.Entity
{
    public class MenuRolPermit : BaseEntity
    {
        public short Id { get; set; }
        public short MenuPermitId { get; set; }
        public short RoleId { get; set; }

        public MenuPermit MenuPermit { get; set; }
        public Role Role { get; set; }
    }
}


