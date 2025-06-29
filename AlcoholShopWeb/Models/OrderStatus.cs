namespace AlcoholShopWeb.Models
{
    public class OrderStatus
    {
        public int StatusID { get; set; }
        public string Name { get; set; }

        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}