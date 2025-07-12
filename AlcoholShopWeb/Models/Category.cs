using System.ComponentModel.DataAnnotations;

namespace AlcoholShopWeb.Models
{

    public class Category
    {
        [Key]
        public int CategoryID { get; set; }
        public string Name { get; set; }

        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}