namespace HBManager.Shared
{
    public class ShoppingListEditDTO
    {
        public int ID { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public int RetailerID { get; set; }
    }
}
