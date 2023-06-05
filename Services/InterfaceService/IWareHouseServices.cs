using ProductManagement.Requests.Params;
using ProductManagement.Requests.WareHouseRequest;
using ProductManagement.Responses;

namespace ProductManagement.Services.InterfaceService
{
    public interface IWareHouseServices
    {
        int Add(WareHouseAddRequest request);

        int Update(WareHouseUpdateRequest request);

        void Delete(int id);

        WareHouseResponse FindById(int id);

        FilterResponse<List<WareHouseResponse>> Search(FilterParams<WareHouseParameters> request);

        FilterResponse<List<WareHouseResponse>> ShowDataWareHouse(FilterParams<WareHouseParameters> request);
    }
}