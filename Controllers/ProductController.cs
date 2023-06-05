using Microsoft.AspNetCore.Mvc;
using ProductManagement.Requests;
using ProductManagement.Requests.Params;
using ProductManagement.Services.InterfaceService;

namespace ProductManagement.Controllers
{
    [ApiController]
    [Route("api/product")]
    public class ProductController : Controller
    {
        private readonly IProductServices _service;

        public ProductController(IProductServices service)
        {
            _service = service;
        }

        [HttpPost("filter")]
        public IActionResult Filter(FilterParams<ProductParameters> request)
        {
            return Ok(_service.Search(request));
        }

        [HttpGet("get-by-id")]
        public IActionResult GetById(int id)
        {
            return Ok(_service.FindBy(id));
        }

        [HttpDelete("delete")]
        public IActionResult Delete(int id)
        {
            _service.Delete(id);
            return Ok();
        }

        [HttpPost("save")]
        public IActionResult Save(ProductAddRequest request)
        {
            var response = _service.Add(request);
            if (response == -1001)
            {
                return Ok("Trùng tên");
            }
            return Ok(response);
        }

        [HttpPut("edit")]
        public IActionResult Edit(ProductUpdateRequest request)
        {
            var response = _service.Update(request);
            if (response == -1001)
            {
                return Ok("Không tìm thấy sản phẩm");
            }
            return Ok("Cập nhật thành công");
        }
    }
}