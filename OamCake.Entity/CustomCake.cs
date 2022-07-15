namespace OamCake.Entity
{
    public class CustomCake : BaseEntity
    {
        public long Id { get; set; }
        public long? CakeId { get; set; }
        public string Photo { get; set; }
        public string Detail { get; set; }

        public Cake? Cake { get; set; }
    }
}
