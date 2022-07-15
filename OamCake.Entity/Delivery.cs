namespace OamCake.Entity
{
    public class Delivery : BaseEntity
    {
        public long Id { get; set; }
        public string VehicleDetail { get; set; }
        public string VehiclePhoto { get; set; }

        public long EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }
}
