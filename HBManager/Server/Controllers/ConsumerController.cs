using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HBManager.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ConsumerController : ControllerBase
    {
        private readonly IConsumerService consumerService;

        public ConsumerController(IConsumerService consumerService)
        {
            this.consumerService = consumerService;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<Consumer>>>> GetConsumersAsync()
        {
            var result = await consumerService.GetConsumersAsync();
            return Ok(result);
        }

        [HttpGet("{consumerID}")]
        public async Task<ActionResult<ServiceResponse<Consumer?>>> GetConsumerByIDAsync(int consumerID)
        {
            var result = await consumerService.GetConsumerByIDAsync(consumerID);
            return Ok(result);
        }

        [HttpGet("{consumerID}/product")]
        public async Task<ActionResult<ServiceResponse<List<Product>>>> GetProductsByConsumerIDAsync(int consumerID)
        {
            var result = await consumerService.GetProductsByConsumerIDAsync(consumerID);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<Consumer?>>> AddConsumerAsync(ConsumerAddDTO consumerDTO)
        {
            var result = await consumerService.AddConsumerAsync(consumerDTO);
            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<Consumer?>>> EditConsumerAsync(ConsumerEditDTO consumerDTO)
        {
            var result = await consumerService.EditConsumerAsync(consumerDTO);
            return Ok(result);
        }


        [HttpDelete("{consumerID}")]
        public async Task<ActionResult<ServiceResponse<bool>>> DeleteConsumerAsync(int consumerID)
        {
            var result = await consumerService.DeleteConsumerAsync(consumerID);
            return Ok(result);
        }



    }
}
