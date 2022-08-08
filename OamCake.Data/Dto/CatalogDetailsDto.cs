namespace OamCake.Data.Dto
{
    public class CatalogDetailsDto
    {
        public long Id { get; set; }
        public decimal Price { get; set; }
        public string Name { get; set; }
        public string? Photo { get; set; }
        public bool IsSet { get; set; }

        public long? CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}
