namespace OamCake.Entity
{
    public class Projection : BaseEntity
    {
        public long Id { get; set; }
        public string Description { get; set; }
        public bool IsOpen { get; set; }

        public IEnumerable<ProjectionDetail> ProjectionDetails { get; set; }
    }
}
