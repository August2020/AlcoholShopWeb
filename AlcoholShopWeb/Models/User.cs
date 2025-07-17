using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AlcoholShopWeb.Models
{
    public class User
    {
        [Key]
        public int UserID { get; set; }

        [Required(ErrorMessage = "Email jest wymagany")]
        [EmailAddress(ErrorMessage = "Niepoprawny email")]
        public string Email { get; set; }

        [BindNever]
        public string PasswordHash { get; set; }

        [Required(ErrorMessage = "Imię jest wymagane")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Nazwisko jest wymagane")]
        public string LastName { get; set; }

        public DateTime? BirthDate { get; set; }

        [BindNever]
        public string Role { get; set; } // 'Admin' lub 'Client'

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}