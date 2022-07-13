namespace OamCake.Entity
{
    public class Ingredient : BaseEntity
    {
        public short Id { get; set; }
        public byte Quantity { get; set; }
        public char Unid { get; set; }
        public short? ProductId { get; set; }
        public short CakeId { get; set; }

        public Product? Product { get; set; }
        public Cake Cake { get; set; }
    }
}
