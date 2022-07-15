namespace OamCake.Entity
{
    public class Cake : BaseEntity
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string? Photo { get; set; }

        public long? CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}
