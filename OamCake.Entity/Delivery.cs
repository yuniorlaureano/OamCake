namespace OamCake.Entity
{
    public class Delivery : BaseEntity
    {
        public short Id { get; set; }
        public int AssignedUserId { get; set; }
        public string VehicleDetail { get; set; }
        public string VehiclePhoto { get; set; }
    }
}
