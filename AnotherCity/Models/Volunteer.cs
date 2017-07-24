using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AnotherCity.Models
{
    public partial class Volunteer : User
    {
        public int? VolunteerOpportunityId { get; set; }

        [Required]
        [MinLength(30)]
        [MaxLength(1024)]
        [Display(Name = "Additional information")]
        public string Bio { get; set; }

        [Url]
        [Display(Name = "Any social link")]
        public string SocialLink { get; set; }
        
        public virtual VolunteerOpportunity VolunteerOpportunity { get; set; }
    }
}
