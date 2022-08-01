namespace OamCake.Entity
{
    public class CatalogDetail : BaseEntity
    {
        public long Id { get; set; }
        public long CakeId { get; set; }
        public long CatalogId { get; set; }
        public decimal Price { get; set; }
        public string Photo { get; set; }

        public Cake Cake { get; set; }
        public Catalog Catalog { get; set; }
    }
}
