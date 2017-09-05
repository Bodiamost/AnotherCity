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
        [Display(Name = "Додаткова інформація")]
        public string Bio { get; set; }

        [Url]
        [Display(Name = "Сторінка в соцмережі")]
        public string SocialLink { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy hh:mm tt}", ApplyFormatInEditMode = true)]
        public DateTime? ApplicationDate { get; set; }

        public virtual VolunteerOpportunity VolunteerOpportunity { get; set; }
    }
}
