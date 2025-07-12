using System.ComponentModel.DataAnnotations;

namespace AlcoholShopWeb.Models
{
    public class Producer
    {
        [Key]
        public int ProducerID { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }

        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}