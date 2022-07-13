namespace OamCake.Entity
{
    public class MenuPermit : BaseEntity
    {
        public int Id { get; set; }
        public short MenuId { get; set; }
        public short PermitId { get; set; }

        public Menu Menu { get; set; }
        public Permit Permit { get; set; }
    }
}
