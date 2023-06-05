using Microsoft.AspNetCore.Mvc;
using ProductManagement.Requests.BrandRequest;
using ProductManagement.Requests.Params;
using ProductManagement.Services.InterfaceService;

namespace ProductManagement.Controllers
{
    [ApiController]
    [Route("api/brand")]
    public class BrandController : Controller
    {
        private readonly IBrandServices _service;

        public BrandController(IBrandServices service)
        {
            _service = service;
        }

        [HttpPost("filter")]
        public IActionResult Filter(FilterParams<BrandParameters> request)
        {
            return Ok(_service.Search(request));
        }

        [HttpGet("Get-By-Id")]
        public IActionResult GetById(int id)
        {
            return Ok(_service.FindById(id));
        }

        [HttpDelete("Delete")]
        public IActionResult Delete(int id)
        {
            _service.Delete(id);
            return Ok();
        }

        [HttpPost("Insert")]
        public IActionResult Add(BrandAddRequet requet)
        {
            var response = _service.Add(requet);
            if (response == -1001)
            {
                return Ok("Trùng tên");
            }
            return Ok(new { id = response, msg = "Thêm Thành Công" });
        }

        [HttpPut("edit")]
        public IActionResult Edit(BrandUpdateRequest request)
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