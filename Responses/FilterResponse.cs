namespace ProductManagement.Responses
{
    public class FilterResponse<T>
    {
        public int TotalRecords { get; set; }
        public T Data { get; set; }
    }
}