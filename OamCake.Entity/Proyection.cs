namespace OamCake.Entity
{
    public class Proyection
    {
        public long Id { get; set; }
        public short CakeId { get; set; }
        public short Quantity { get; set; }

        public Cake Cake { get; set; }
    }
}
