namespace OamCake.Entity
{
    public class MadeCake : BaseEntity
    {
        public long Id { get; set; }
        public long CakeId { get; set; }
        public DateTime Date { get; set; }
        public char Status { get; set; }
    }
}
