namespace AlcoholShopWeb.Models
{
    public class ProductionMethod
    {
        public int ProductionMethodID { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }

        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}