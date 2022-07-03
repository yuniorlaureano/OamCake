namespace OamCake.Entity
{
    public class Order
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public decimal Payment { get; set; }
        public char Status { get; set; }
        public long ClientId { get; set; }

        public User User { get; set; }
        public Client Client { get; set; }
    }
}
