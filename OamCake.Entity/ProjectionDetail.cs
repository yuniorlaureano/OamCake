namespace OamCake.Entity
{
    public class ProjectionDetail : BaseEntity
    {
        public long Id { get; set; }
        public long CakeId { get; set; }
        public long ProjectionId { get; set; }
        public int Quantity { get; set; }

        public Cake Cake { get; set; }
        public Projection Projection { get; set; }
    }
}
