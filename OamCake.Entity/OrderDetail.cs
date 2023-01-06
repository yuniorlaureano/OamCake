namespace OamCake.Entity
{
    public class OrderDetail : BaseEntity
    {
        public long Id { get; set; }
        public long? CakeId { get; set; }
        public long OrderId { get; set; }
        public long? CustomCakeId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public CustomCake? CustomCake { get; set; }
        public Cake? Cake { get; set; }
        public Order Order { get; set; }
    }
}
