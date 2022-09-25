namespace OamCake.Data.Dto
{
    public class ProjectionCreationDto
    {
        public long Id { get; set; }
        public string Description { get; set; }
        public bool IsOpen { get; set; }
        public string[] CakesId { get; set; }
    }
}
