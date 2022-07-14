namespace OamCake.Entity
{
    public class Order : BaseEntity
    {
        public long Id { get; set; }
        public decimal Payment { get; set; }
        public char Status { get; set; }
        public long ClientId { get; set; }

        public Client Client { get; set; }
    }
}
