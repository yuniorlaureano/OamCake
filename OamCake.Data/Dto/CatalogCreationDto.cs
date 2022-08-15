namespace OamCake.Data.Dto
{
    public class CatalogCreationDto
    {
        public long Id { get; set; }
        public string Description { get; set; }
        public bool IsPublished { get; set; }
        public long[] CakesId { get; set; }
    }
}
