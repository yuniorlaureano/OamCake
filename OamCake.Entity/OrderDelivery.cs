namespace OamCake.Entity
{
    public class OrderDelivery : BaseEntity
    {
        public long Id { get; set; }
        public long OrderId { get; set; }
        public long DeliveryId { get; set; }
        public char Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public Order Order { get; set; }
        public Delivery Delivery { get; set; }
    }
}
