namespace OamCake.Data.Dto
{
    public class CatalogDetailListDto
    {
        public long Id { get; set; }
        public long CatalogId { get; set; }
        public decimal Price { get; set; }
        public string Photo { get; set; }


        public long CakeId { get; set; }
        public string CakeName { get; set; }
        public string? RealPhoto { get; set; }
        public long? CategoryId { get; set; }

    }
}
