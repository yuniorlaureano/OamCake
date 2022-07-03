namespace OamCake.Entity
{
    public class OrderDetail
    {
        public long Id { get; set; }
        public long CakeId { get; set; }
        public long OrderId { get; set; }
        public long CustomCakeId { get; set; }
        public short Quantity { get; set; }

        public CustomCake CustomCake { get; set; }
        public Cake Cake { get; set; }
        public Order Order { get; set; }
    }
}
