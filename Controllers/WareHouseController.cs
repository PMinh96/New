using Microsoft.AspNetCore.Mvc;
using ProductManagement.Requests.Params;
using ProductManagement.Requests.WareHouseRequest;
using ProductManagement.Services.InterfaceService;

namespace ProductManagement.Controllers
{
    [ApiController]
    [Route("api/WareHouse")]
    public class WareHouseController : Controller
    {
        private readonly IWareHouseServices _service;

        public WareHouseController(IWareHouseServices service)
        {
            _service = service;
        }

        [HttpPost("filter")]
        public IActionResult Filter(FilterParams<WareHouseParameters> request)
        {
            return Ok(_service.Search(request));
        }

        [HttpPost("save")]
        public IActionResult Save(WareHouseAddRequest request)

        {
            var response = _service.Add(request);
            if (response == -1001)
            {
                return Ok("Trùng tên");
            }
            return Ok(response);
        }

        [HttpDelete("delete")]
        public IActionResult Delete(int id)
        {
            _service.Delete(id);
            return Ok();
        }

        [HttpPut("edit")]
        public IActionResult Edit(WareHouseUpdateRequest request)
        {
            var response = _service.Update(request);
            if (response == -1001)
            {
                return Ok("Không tìm thấy sản phẩm");
            }
            return Ok("Cập nhật thành công");
        }

        [HttpPost("show")]
        public IActionResult ShowDataWareHouse(FilterParams<WareHouseParameters> request)
        {
            return Ok(_service.ShowDataWareHouse(request));
        }
    }
}