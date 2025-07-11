
using System.ComponentModel.DataAnnotations;

namespace AlcoholShopWeb.Models.ViewModels
{
    public class OrderFormViewModel
    {
        public int UserId { get; set; }

        [Required]
        public int DeliveryMethodID { get; set; }

        [Required]
        public int PaymentMethodID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
