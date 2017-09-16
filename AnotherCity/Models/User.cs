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
        [Display(Name = "FirstName")]
        public string FirstName { get; set; }
        [Required]
        [RegularExpression(@"([A-Z][a-z]+)", ErrorMessage = "Please enter correct name!")]
        [Display(Name = "LastName")]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Required]
        [Phone]
        [Display(Name = "Phone")]
        public string Phone { get; set; }

        [Display(Name = "Аккаунт ID")]
        public int? AccountId { get; set; }

        [Display(Name = "Аккаунт")]
        public virtual Account Account { get; set; }
    }
}
