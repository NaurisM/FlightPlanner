namespace FlightPlanner.Core.Models
{
    public class PageResult<T>
    {
        public int Page { get; set; }
        public int TotalItems { get; set; }
        public T[] Items { get; set; }
    }
}