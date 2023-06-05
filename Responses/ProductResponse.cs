namespace ProductManagement.Responses
{
    public class ProductResponse : BaseResponse
    {
        public int? BrandId { get; set; }
        public string BrandName { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}