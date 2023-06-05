namespace ProductManagement.Responses
{
    public class WareHouseResponse : BaseResponse
    {
        public string Name { get; set; }

        public string Address { get; set; }

        public List<ProductResponse> Products { get; set; }
    }
}