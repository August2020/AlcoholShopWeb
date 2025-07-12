using System.ComponentModel.DataAnnotations;

namespace AlcoholShopWeb.Models
{
    public class PaymentMethod
    {
        [Key]
        public int PaymentMethodID { get; set; }
        public string Name { get; set; }

        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}