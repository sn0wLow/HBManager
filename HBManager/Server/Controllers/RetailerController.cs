using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HBManager.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RetailerController : ControllerBase
    {
        private readonly IRetailerService retailerService;

        public RetailerController(IRetailerService retailerService)
        {
            this.retailerService = retailerService;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<Retailer>>>> GetRetailersAsync()
        {
            var result = await retailerService.GetRetailersAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<Retailer?>>> GetRetailerByIDAsync(int id)
        {
            var result = await retailerService.GetRetailerByIDAsync(id);
            return Ok(result);
        }

        [HttpGet("with-product-info")]
        public async Task<ActionResult<ServiceResponse<List<Retailer>>>> GetRetailersWithProductInfoAsync()
        {
            var result = await retailerService.GetRetailersWithProductInfoAsync();
            return Ok(result);
        }
    }
}
