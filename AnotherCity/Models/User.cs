using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AnotherCity.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        [RegularExpression(@"([A-Z][a-z]+)",ErrorMessage ="Please enter correct name!")]
        [Display(Name = "Ім'я")]
        public string FirstName { get; set; }
        [Required]
        [RegularExpression(@"([A-Z][a-z]+)", ErrorMessage = "Please enter correct name!")]
        [Display(Name = "Прізвище")]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Required]
        [Phone]
        [Display(Name = "Телефон")]
        public string Phone { get; set; }

        [Display(Name = "Аккаунт")]
        public int? AccountId { get; set; }

        public virtual Account Account { get; set; }
    }
}
