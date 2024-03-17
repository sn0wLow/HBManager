namespace HBManager.Client
{
    public interface IConsumerService
    {
        List<Consumer> Consumers { get; set; }
        Task GetConsumersAsync();
        Task<ServiceResponse<Consumer?>> GetConsumerByIDAsync(int consumerID, bool includeProducts = true);
        Task<ServiceResponse<Consumer?>> AddConsumerAsync(ConsumerAddDTO consumerDTO);
        Task<ServiceResponse<Consumer?>> EditConsumerAsync(ConsumerEditDTO consumerDTO);
        Task<ServiceResponse<bool>> DeleteConsumerAsync(int consumerID);
    }
}
