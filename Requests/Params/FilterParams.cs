namespace ProductManagement.Requests.Params
{
    public class FilterParams<T>
    {
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public T ObjectFilter { get; set; }
    }
}