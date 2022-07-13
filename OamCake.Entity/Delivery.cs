namespace OamCake.Entity
{
    public class Delivery : BaseEntity
    {
        public short Id { get; set; }
        public long? UserId { get; set; }
        public string VehicleDetail { get; set; }
        public string VehiclePhoto { get; set; }

        public User? User { get; set; }
    }
}
