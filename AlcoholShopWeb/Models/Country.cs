using System.ComponentModel.DataAnnotations;

namespace AlcoholShopWeb.Models
{
    public class Country
    {
        [Key]
        public int CountryID { get; set; }
        public string Name { get; set; }

        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}