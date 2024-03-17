namespace HBManager.Client
{
    public interface IRetailerService
    {
        List<Retailer> Retailers { get; set; }
        Task GetRetailersAsync();
        Task<ServiceResponse<Retailer?>> GetRetailerByIDAsync(int id);
        Task<ServiceResponse<List<Retailer>>> GetRetailersWithProductInfoAsync();
    }
}
