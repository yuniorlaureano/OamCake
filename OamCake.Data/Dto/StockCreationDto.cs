namespace OamCake.Data.Dto
{
    public class StockCreationDto
    {
        public int ProductId { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int OldQuantity { get; set; }

        public bool IsValid()
        {
            return Quantity > 0;
        }
    }
}
