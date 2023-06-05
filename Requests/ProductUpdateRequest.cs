namespace ProductManagement.Requests
{
    public class ProductUpdateRequest : BaseRequest
    {
        public int Quantity { get; set; }

        public decimal Price { get; set; }
    }
}