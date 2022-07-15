namespace OamCake.Entity
{
    public class MenuPermit : BaseEntity
    {
        public long Id { get; set; }
        public long MenuId { get; set; }
        public long PermitId { get; set; }

        public Menu Menu { get; set; }
        public Permit Permit { get; set; }
    }
}
