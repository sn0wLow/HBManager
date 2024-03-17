namespace HBManager.Server
{
    public interface IRetailerService
    {
        Task<ServiceResponse<List<Retailer>>> GetRetailersAsync();
        Task<ServiceResponse<Retailer?>> GetRetailerByIDAsync(int id);
        Task<ServiceResponse<List<Retailer>>> GetRetailersWithProductInfoAsync();
    }
}
