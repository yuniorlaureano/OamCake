namespace OamCake.Entity
{
    public class Cake : BaseEntity
    {
        public short Id { get; set; }
        public string Name { get; set; }
        public string? Photo { get; set; }

        public short? CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}
