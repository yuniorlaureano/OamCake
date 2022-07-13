namespace OamCake.Entity
{
    public class Catalog : BaseEntity
    {
        public long Id { get; set; }
        public short CakeId { get; set; }
        public decimal Price { get; set; }
        public string Photo { get; set; }

        public Cake Cake { get; set; }
    }
}
