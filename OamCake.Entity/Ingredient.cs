namespace OamCake.Entity
{
    public class Ingredient : BaseEntity
    {
        public long Id { get; set; }
        public int Quantity { get; set; }
        public char Unid { get; set; }
        public long? ProductId { get; set; }
        public long CakeId { get; set; }

        public Product? Product { get; set; }
        public Cake Cake { get; set; }
    }
}
