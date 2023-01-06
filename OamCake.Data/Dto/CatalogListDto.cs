namespace OamCake.Data.Dto
{
    public class CatalogListDto
    {
        public long Id { get; set; }
        public string Description { get; set; }

        public List<CatalogDetailListDto> CatalogDetailListDto { get; set; }
    }
}
