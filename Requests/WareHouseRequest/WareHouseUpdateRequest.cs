namespace ProductManagement.Requests.WareHouseRequest
{
    public class WareHouseUpdateRequest : BaseRequest
    {
        public string Name { get; set; }
        public string Address { set; get; }
    }
}
