namespace OamCake.Data.Dto
{
    public class TableResponse<T>
    {
        public List<T> Data { get; set; }
        public int Count { get; set; }
        public int Page { get; set; }

        public int CalcPage(int baseCount)
        {
            if (Count == 0)
            {
                return 0;
            }

            if (Count % baseCount == 0)
            {
                return Count / baseCount;
            }
            else
            {
                var pages = (int)(Count / baseCount);
                return pages + 1;
            }
        }
    }
}
