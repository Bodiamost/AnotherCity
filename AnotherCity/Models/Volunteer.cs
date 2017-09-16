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
        [Display(Name = "AdditionalInfo")]
        public string Bio { get; set; }

        [Url]
        [Display(Name = "SocialLink")]
        public string SocialLink { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy hh:mm tt}", ApplyFormatInEditMode = true)]
        [Display(Name = "Дата подачі")]
        public DateTime? ApplicationDate { get; set; }

        [Display(Name = "Проект")]
        public virtual VolunteerOpportunity VolunteerOpportunity { get; set; }
    }
}
