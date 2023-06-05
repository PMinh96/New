namespace ProductManagement.Requests
{
    public class ProductAddRequest : BaseRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Name => $"{FirstName} {LastName}";

        public int BrandId { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }
    }
}