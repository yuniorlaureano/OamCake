namespace OamCake.Entity
{
    public class Product : BaseEntity
    {
        public short Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
