namespace OamCake.Entity
{
    public class Catalog : BaseEntity
    {
        public long Id { get; set; }
        public string Description { get; set; }
        public bool IsPublished { get; set; }
        public DateTime PublishDate { get; set; }

        public List<CatalogDetail> CatalogDetails { get; set; }
    }
}
