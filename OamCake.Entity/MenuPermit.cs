namespace OamCake.Entity
{
    public class MenuPermit
    {
        public int Id { get; set; }
        public byte MenuId { get; set; }
        public short PermitId { get; set; }

        public Menu Menu { get; set; }
        public Permit Permit { get; set; }
    }
}
