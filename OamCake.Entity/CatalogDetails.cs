namespace OamCake.Entity
{
    public class CatalogDetails : BaseEntity
    {
        public long Id { get; set; }
        public short CakeId { get; set; }
        public long CatalogId { get; set; }
        public decimal Price { get; set; }
        public string Photo { get; set; }

        public Cake Cake { get; set; }
        public Catalog Catalog { get; set; }
    }
}
