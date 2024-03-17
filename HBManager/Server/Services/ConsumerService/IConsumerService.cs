namespace HBManager.Server
{
    public interface IConsumerService
    {

        Task<ServiceResponse<List<Consumer>>> GetConsumersAsync();
        Task<ServiceResponse<Consumer?>> GetConsumerByIDAsync(int consumerID);   
        Task<ServiceResponse<List<Product>>> GetProductsByConsumerIDAsync(int consumerID);   
        Task<ServiceResponse<Consumer?>> AddConsumerAsync(ConsumerAddDTO consumerDTO);
        Task<ServiceResponse<Consumer?>> EditConsumerAsync(ConsumerEditDTO consumerDTO);
        Task<ServiceResponse<bool>> DeleteConsumerAsync(int consumerID);
    }
}
