namespace OamCake.Entity
{
    public class Menu : BaseEntity
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Path { get; set; }
        public string Icon { get; set; }
    }
}
