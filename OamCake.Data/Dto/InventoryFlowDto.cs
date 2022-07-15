namespace OamCake.Data.Dto
{
    public enum Flow
    {
        Input,
        Output,
    }

    public class InventoryFlowDto
    {
        public Flow Flow { get; set; }
        public IEnumerable<StockDto> Stock { get; set; }
    }
}
