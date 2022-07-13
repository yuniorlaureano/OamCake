namespace OamCake.Entity
{
    public class InventoryProvider : BaseEntity
    {
        public long Id { get; set; }
        public string Description { get; set; }
        public char Mode { get; set; }
        public decimal TotalPrice { get; set; }
        public short ProviderId { get; set; }

        public Provider Provider { get; set; }
    }
}
