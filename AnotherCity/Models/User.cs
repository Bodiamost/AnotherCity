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
        [Display(Name = "First name")]
        public string FirstName { get; set; }
        [Required]
        [RegularExpression(@"([A-Z][a-z]+)", ErrorMessage = "Please enter correct name!")]
        [Display(Name = "Last name")]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Contact email")]
        public string Email { get; set; }
        [Required]
        [Phone]
        [Display(Name = "Contact phone")]
        public string Phone { get; set; }

        [Display(Name = "Account")]
        public int? AccountId { get; set; }

        public virtual Account Account { get; set; }
    }
}
