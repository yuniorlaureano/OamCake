namespace OamCake.Data.Dto
{
    public class ProjectionCreationDto
    {
        public long Id { get; set; }
        public string Description { get; set; }
        public bool IsOpen { get; set; }
        public string[] CakesId { get; set; }

        public ProjectQuantityAndIdsDto[] AsProjectionQuantityAndIds()
        {
            return CakesId.Select(x => new ProjectQuantityAndIdsDto(x)).ToArray();
        }
    }

    public class ProjectQuantityAndIdsDto
    {
        public ProjectQuantityAndIdsDto(string value)
        {
            var splitedValue = value.Split("|");
            Id = long.Parse(splitedValue[0]);
            Quantity = int.Parse(splitedValue[1]);
        }

        public long Id { get; set; }
        public int Quantity { get; set; }
    }
}
