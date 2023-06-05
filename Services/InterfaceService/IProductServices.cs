using ProductManagement.Requests;
using ProductManagement.Requests.Params;
using ProductManagement.Responses;

namespace ProductManagement.Services.InterfaceService
{
    /// <summary>
    /// CRUD: Create, Read || Recieve, Update, Delete
    /// </summary>
    public interface IProductServices
    {
        int Add(ProductAddRequest request);

        int Update(ProductUpdateRequest request);

        void Delete(int id);

        ProductResponse FindBy(int id);

        FilterResponse<List<ProductResponse>> Search(FilterParams<ProductParameters> request);
    }
}