using System.ComponentModel.DataAnnotations;

namespace AlcoholShopWeb.Models
{
    public class OrderStatus
    {
        [Key]
        public int StatusID { get; set; }
        public string Name { get; set; }

        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}