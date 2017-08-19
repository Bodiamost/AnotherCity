using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AnotherCity.Models
{
    public partial class Project
    {
        public Project()
        {
            Images = new HashSet<Image>();
            InvestOpportunities = new HashSet<InvestOpportunity>();
            VolunteerOpportunities = new HashSet<VolunteerOpportunity>();
        }

        public int Id { get; set; }
        [Required]
        public int? MemberId { get; set; }
        [Required]
        public string Title { get; set; }
        public bool VolunteerOpp { get; set; }
        public bool InvestOpp { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy hh:mm tt}", ApplyFormatInEditMode = true)]
        public DateTime? StartDate { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy hh:mm tt}", ApplyFormatInEditMode = true)]
        public DateTime? FinishDate { get; set; }
        [Required]
        [MinLength(5)]
        [MaxLength(128)]
        public string ShortDesc { get; set; }
        [Required]
        [MinLength(5)]
        [MaxLength(1024)]
        public string Description { get; set; }
        public string MainImg { get; set; }
        [Required]
        public string Location { get; set; }
        public string LatLng { get; set; }
        public string SocialLinks { get; set; }

        [Required]
        public string Status { get; set; }

        public bool Featured { get; set; }

        public virtual ICollection<Image> Images { get; set; }
        public virtual ICollection<InvestOpportunity> InvestOpportunities { get; set; }
        public virtual ICollection<VolunteerOpportunity> VolunteerOpportunities { get; set; }
        public virtual Member Member { get; set; }
        public ICollection<ProjectSocials> ProjectSocials { get; set; }

    }
}
