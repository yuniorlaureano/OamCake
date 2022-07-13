namespace OamCake.Entity
{
    public class Role : BaseEntity
    {
        public short Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public char Status { get; set; }
    }
}
