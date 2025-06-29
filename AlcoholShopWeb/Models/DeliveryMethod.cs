namespace AlcoholShopWeb.Models
{
    public class DeliveryMethod
    {
        public int DeliveryMethodID { get; set; }
        public string Name { get; set; }

        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}