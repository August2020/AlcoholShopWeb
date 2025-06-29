namespace AlcoholShopWeb.Models
{
    public class Producer
    {
        public int ProducerID { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }

        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}