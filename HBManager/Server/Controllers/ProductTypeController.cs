using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HBManager.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductTypeController : ControllerBase
    {
        private readonly IProductTypeService productTypeService;

        public ProductTypeController(IProductTypeService productTypeService)
        {
            this.productTypeService = productTypeService;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<ProductType>>>> GetProductTypesAsync()
        {
            var result = await productTypeService.GetProductTypesAsync();
            return Ok(result);
        }
    }
}
