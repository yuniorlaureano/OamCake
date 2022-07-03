namespace OamCake.Entity
{
    public class Inventory
    {
        public long Id { get; set; }
        public long IventoryProviderId { get; set; }
        public short ProductId { get; set; }
        public DateTime Date { get; set; }
        public short Quantity { get; set; }
        public char Action { get; set; }

        public Product Product { get; set; }
        public InventoryProvider InventoryProvider { get; set; }
    }
}
