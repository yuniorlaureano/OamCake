namespace OamCake.Data.Dto
{
    public class MovementSummaryDto
    {
        public long ProductId { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public int OutputQty { get; set; }
        public int InputQty { get; set; }
    }
}
