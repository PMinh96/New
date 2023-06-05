using ProductManagement.Requests.BrandRequest;
using ProductManagement.Requests.Params;
using ProductManagement.Responses;

namespace ProductManagement.Services.InterfaceService
{
    public interface IBrandServices
    {
        int Add(BrandAddRequet request);

        int Update(BrandUpdateRequest request);

        void Delete(int id);

        BrandResponse FindById(int id);

        FilterResponse<List<BrandResponse>> Search(FilterParams<BrandParameters> request);
    }
}