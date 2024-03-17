namespace HBManager.Shared
{
    public class ShoppingListAddDTO
    {
        public DateTime Date { get; set; } = DateTime.Now;
        public int ShoppingListCatalogID { get; set; }
        public int RetailerID { get; set; } = 1;
    }
}
