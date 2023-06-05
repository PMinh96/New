namespace ProductManagement.Requests.WareHouseRequest
{
    public class WareHouseAddRequest : BaseRequest
    {
        public string Name { get; set; }

        public string Address { set; get; }
    }
}
