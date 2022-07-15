namespace OamCake.Data.Dto
{
    public class StockDto
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int OutputQty { get; set; }
        public int InputQty { get; set; }
        public int InStock { get; set; }
    }
}