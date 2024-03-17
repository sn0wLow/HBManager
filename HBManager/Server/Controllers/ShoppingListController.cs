using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace HBManager.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ShoppingListController : ControllerBase
    {
        private readonly IShoppingListService shoppingListService;

        public ShoppingListController(IShoppingListService shoppingListService)
        {
            this.shoppingListService = shoppingListService;
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<ShoppingList>>> GetShoppingListByIDAsync(int id)
        {
            var result = await shoppingListService.GetShoppingListByIDAsync(id);
            return Ok(result);
        }        

        [HttpDelete("{shoppingListID}")]
        public async Task<ActionResult<ServiceResponse<bool>>> DeleteShoppingList(int shoppingListID)
        {
            var result = await shoppingListService.DeleteShoppingList(shoppingListID);
            return Ok(result);
        }

        [HttpDelete("product/{productID}")]
        public async Task<ActionResult<ServiceResponse<bool>>> DeleteProduct(int productID)
        {
            var result = await shoppingListService.DeleteProduct(productID);
            return Ok(result);
        }

        [HttpPut("product")]
        public async Task<ActionResult<ServiceResponse<Product?>>> EditProduct(ProductEditDTO productDTO)
        {
            var result = await shoppingListService.EditProduct(productDTO);
            return Ok(result);
        }

        [HttpPost("product")]
        public async Task<ActionResult<ServiceResponse<Product?>>> AddProduct(ProductAddDTO productDTO)
        {
            var result = await shoppingListService.AddProduct(productDTO);
            return Ok(result);
        }

        

        [HttpGet("product/mostUsedProducts")]
        public async Task<ActionResult<ServiceResponse<List<ProductSearchDTO>>>> GetMostUsedProductsAsync([FromQuery, MaxLength(25)] string searchString = "") 
        {
            var result = await shoppingListService.GetMostUsedProductsAsync(searchString);
            return Ok(result);
        }

        [HttpGet("product/titles")]
        public async Task<ActionResult<ServiceResponse<List<string>>>> GetMostUsedProductTitlesAsync([FromQuery, MaxLength(25)] string searchString = "")
        {
            await Console.Out.WriteLineAsync(searchString);
            var result = await shoppingListService.GetMostUsedProductTitlesAsync(searchString);
            return Ok(result);
        }

        [HttpGet("product/search")]
        public async Task<ActionResult<ServiceResponse<List<ProductSearchDTO>>>> GetProductSearchItemsAsync([FromQuery, MaxLength(25)] string searchString = "")
        {
            var result = await shoppingListService.GetProductSearchItemsAsync(searchString);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<ShoppingList?>>> AddShoppingList(ShoppingListAddDTO shoppingListDTO)
        {
            var result = await shoppingListService.AddShoppingList(shoppingListDTO);
            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<ShoppingList?>>> EditShoppingList(ShoppingListEditDTO shoppingListDTO)
        {
            var result = await shoppingListService.EditShoppingList(shoppingListDTO);
            return Ok(result);
        }


    }
}
