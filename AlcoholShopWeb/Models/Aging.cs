namespace AlcoholShopWeb.Models
{
    public class Aging
    {
        public int AgingID { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }

        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}