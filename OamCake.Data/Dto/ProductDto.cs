using OamCake.Entity;

namespace OamCake.Data.Dto
{
    public class ProductDto
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }

        public ProductDto() { }

        public ProductDto(Product product)
        {
            Code = product.Code;
            Name = product.Name;
            Description = product.Description;
            Id = product.Id;
            Price = product.Price;
        }
    }
}
