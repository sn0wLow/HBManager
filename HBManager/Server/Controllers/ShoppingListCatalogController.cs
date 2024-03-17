using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HBManager.Server
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ShoppingListCatalogController : ControllerBase
    {
        private readonly IShoppingListCatalogService shoppingListCatalogService;

        public ShoppingListCatalogController(IShoppingListCatalogService shoppingListService)
        {
            this.shoppingListCatalogService = shoppingListService;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<ShoppingList>>>> GetShoppingListCatalogsAsync()
        {
            var result = await shoppingListCatalogService.GetShoppingListCatalogsAsync();
            return Ok(result);
        }

        [HttpGet("{year}")]
        public async Task<ActionResult<ServiceResponse<List<ShoppingListCatalog>>>> GetShoppingListCatalogsByYearAsync(int year)
        {
            var result = await shoppingListCatalogService.GetShoppingListCatalogsByYearAsync(year);
            return Ok(result);
        }

        [HttpGet("{year}/{month}")]
        public async Task<ActionResult<ServiceResponse<ShoppingListCatalog>>> GetShoppingListCatalogByYearMonthAsync(int year, int month)
        {
            var result = await shoppingListCatalogService.GetShoppingListCatalogByYearMonthAsync(year, month);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<ShoppingListCatalog?>>> AddShoppingListCatalog(ShoppingListCatalogAddDTO shoppingListCatalogDTO)
        {
            var result = await shoppingListCatalogService.AddShoppingListCatalog(shoppingListCatalogDTO);
            return Ok(result);
        }


        [HttpDelete("{shoppingListCatalogID}")]
        public async Task<ActionResult<ServiceResponse<bool>>> DeleteShoppingListCatalog(int shoppingListCatalogID)
        {
            var result = await shoppingListCatalogService.DeleteShoppingListCatalog(shoppingListCatalogID);
            return Ok(result);
        }


        [HttpPut]
        public async Task<ActionResult<ServiceResponse<ShoppingListCatalog?>>> UpdateShoppingListCatalog(ShoppingListCatalogEditDTO shoppingListCatalogDTO)
        {
            var result = await shoppingListCatalogService.EditShoppingListCatalog(shoppingListCatalogDTO);
            return Ok(result);
        }

    }
}
