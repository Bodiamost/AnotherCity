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
        [Display(Name = "Title")]
        public string Title { get; set; }
        [Display(Name = "Volunteers?")]
        public bool VolunteerOpp { get; set; }
        [Display(Name = "Investors?")]
        public bool InvestOpp { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy hh:mm tt}", ApplyFormatInEditMode = true)]
        [Display(Name = "Start")]
        public DateTime? StartDate { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Finish")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy hh:mm tt}", ApplyFormatInEditMode = true)]
        public DateTime? FinishDate { get; set; }
        [Required]
        [MinLength(5)]
        [MaxLength(128)]
        [Display(Name = "Short description")]
        public string ShortDesc { get; set; }
        [Required]
        [MinLength(5)]
        [MaxLength(4096)]
        [Display(Name = "Description")]
        public string Description { get; set; }
        [Display(Name = "Main image")]
        public string MainImg { get; set; }
        [Required]
        [Display(Name = "Location")]
        public string Location { get; set; }
        [Display(Name = "Map Location")]
        public string LatLng { get; set; }
        [Display(Name = "Social Links")]
        public string SocialLinks { get; set; }

        [Required]
        [Display(Name = "Status")]
        public string Status { get; set; }

        [Display(Name = "Featured?")]
        public bool Featured { get; set; }

        [Display(Name = "Fotos")]
        public virtual ICollection<Image> Images { get; set; }
        [Display(Name = "Volunteers")]
        public virtual ICollection<InvestOpportunity> InvestOpportunities { get; set; }
        [Display(Name = "Investors")]
        public virtual ICollection<VolunteerOpportunity> VolunteerOpportunities { get; set; }
        [Display(Name = "Responsible")]
        public virtual Member Member { get; set; }
        public ICollection<ProjectSocials> ProjectSocials { get; set; }

    }
}
