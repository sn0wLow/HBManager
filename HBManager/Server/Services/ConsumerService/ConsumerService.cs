using HBManager.Shared;

namespace HBManager.Server
{
    public class ConsumerService : IConsumerService
    {
        private readonly DataContext context;
        private readonly IAuthService authService;

        public ConsumerService(DataContext context, IAuthService authService)
        {
            this.context = context;
            this.authService = authService;
        }

        public async Task<ServiceResponse<Consumer?>> AddConsumerAsync(ConsumerAddDTO consumerDTO)
        {
            var userID = await this.authService.GetUserID();

            var consumers = this.context.Consumers.Where(x => x.UserID == userID);

            if (consumers.Count() >= 12)
            {
                return new ServiceResponse<Consumer?>
                {
                    Success = false,
                    Data = null,
                    Message = "Es existieren bereits 12 Verbraucher."
                };
            }

            var consumer = new Consumer
            {
                ColorIndex = consumerDTO.ColorIndex,
                Name = consumerDTO.Name,
                UserID = userID
            };

            this.context.Consumers.Add(consumer);

            await this.context.SaveChangesAsync();

            return new ServiceResponse<Consumer?> { Data = consumer, Success = true };
        }

        public async Task<ServiceResponse<Consumer?>> EditConsumerAsync(ConsumerEditDTO consumerDTO)
        {
            var userID = await this.authService.GetUserID();

            var consumer = await this.context.Consumers.FirstOrDefaultAsync(x => x.UserID == userID && x.ID == consumerDTO.ID);

            if (consumer is null)
            {
                return new ServiceResponse<Consumer?>
                {
                    Success = false,
                    Data = null,
                    Message = "Dieser Verbraucher existiert nicht."
                };
            }

            consumer.Name = consumerDTO.Name;
            consumer.ColorIndex = consumerDTO.ColorIndex;

            await context.SaveChangesAsync();

            return new ServiceResponse<Consumer?> { Data = consumer, Success = true };
        }

        public async Task<ServiceResponse<bool>> DeleteConsumerAsync(int consumerID)
        {
            var userID = await this.authService.GetUserID();

            var consumer = await this.context.Consumers.FirstOrDefaultAsync(x => x.UserID == userID && x.ID == consumerID);

            if (consumer is null)
            {
                return new ServiceResponse<bool>
                {
                    Success = false,
                    Data = false,
                    Message = "Dieser Verbraucher existiert nicht."
                };
            }

            await this.context.Products.Where(x => x.Consumers.Any(x => x.ID == consumer.ID))
                .ForEachAsync(x => { x.Consumers.Remove(consumer); });
            this.context.Consumers.Remove(consumer);

            await this.context.SaveChangesAsync();
            return new ServiceResponse<bool> { Data = true, Success = true };
        }



        public async Task<ServiceResponse<Consumer?>> GetConsumerByIDAsync(int consumerID)
        {
            var userID = await this.authService.GetUserID();



            var consumer = await this.context.Consumers
                .FirstOrDefaultAsync(x => x.UserID == userID && x.ID == consumerID);

            if (consumer is null)
            {
                return new ServiceResponse<Consumer?>
                {
                    Success = false,
                    Data = null,
                    Message = "Dieser Verbraucher existiert nicht."
                };
            }

            return new ServiceResponse<Consumer?> { Data = consumer, Success = true };
        }


        public async Task<ServiceResponse<List<Consumer>>> GetConsumersAsync()
        {
            var userID = await this.authService.GetUserID();

            List<Consumer> consumers = await this.context.Consumers
                .Where(x => x.UserID == userID)
                .Include(x => x.Products)
                .ThenInclude(x => x.Consumers)
                .ToListAsync();

            int globalTotalUniqueProductAmount = consumers.SelectMany(x => x.Products).DistinctBy(x => x.ID).Count();

            foreach (var consumer in consumers)
            {
                if (consumer.Products.Any())
                {
                    consumer.GlobalTotalUniqueProductAmount = globalTotalUniqueProductAmount;
                    consumer.TotalProductAmount = consumer.Products.Count;
                    consumer.TotalProductPrice = (double)consumer.Products.Sum(x => (x.Price * x.Quantity) / x.Consumers.Count);
                    consumer.AverageProductPrice = (double)consumer.Products.Average(x => x.Price);
                    consumer.HasAnyProducts = consumer.TotalProductAmount > 0;
                }
            }

            return new ServiceResponse<List<Consumer>> { Data = consumers, Success = true };
        }

        public async Task<ServiceResponse<List<Product>>> GetProductsByConsumerIDAsync(int consumerID)
        {
            var userID = await this.authService.GetUserID();

            var consumer = await this.context.Consumers
                .FirstOrDefaultAsync(x => x.UserID == userID && x.ID == consumerID);

            if (consumer is null)
            {
                return new ServiceResponse<List<Product>>
                {
                    Success = false,
                    Data = null,
                    Message = "Dieser Verbraucher existiert nicht."
                };
            }

            var products = await context.Products
                .Where(x => x.Consumers.Any(x => x.UserID == userID && x.ID == consumerID))
                .Include(x => x.Consumers)
                .Include(x => x.ProductType)
                .ToListAsync();


            return new ServiceResponse<List<Product>> { Data = products, Success = true };
        }
    }
}
