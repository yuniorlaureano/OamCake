namespace OamCake.Entity
{
    public class Order : BaseEntity
    {
        public long Id { get; set; }
        public decimal Payment { get; set; }
        public char Status { get; set; }

        public List<OrderDetail> OrderDetails { get; set; }
    }
}
