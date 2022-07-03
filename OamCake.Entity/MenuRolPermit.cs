namespace OamCake.Entity
{
    public class MenuRolPermit
    {
        public short Id { get; set; }
        public byte MenuPermitId { get; set; }
        public byte RoleId { get; set; }

        public MenuPermit MenuPermit { get; set; }
        public Role Role { get; set; }
    }
}


