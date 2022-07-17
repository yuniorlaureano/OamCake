namespace OamCake.Entity
{
    public class Inventory : BaseEntity
    {
        public long Id { get; set; }
        public long IventoryProviderId { get; set; }
        public long ProductId { get; set; }
        public DateTime Date { get; set; }
        public int Quantity { get; set; }

        public Product Product { get; set; }
    }
}
