using System.ComponentModel.DataAnnotations;

namespace AlcoholShopWeb.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Hasła się nie zgadzają.")]
        public string ConfirmPassword { get; set; }
    }
}
