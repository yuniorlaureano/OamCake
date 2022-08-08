namespace OamCake.Data.Dto
{
    public class CatalogDetailsCakes
    {
        public long CakeId { get; set; }
        public decimal Price { get; set; }
    }

    public class CatalogCreationDto
    {
        public long Id { get; set; }
        public string Description { get; set; }
        public bool IsPublished { get; set; }
        public CatalogDetailsCakes[] CakesId { get; set; }
    }
}
