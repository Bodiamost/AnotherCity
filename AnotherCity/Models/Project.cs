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
        [Display(Name = "Назва")]
        public string Title { get; set; }
        [Display(Name = "Волонтери?")]
        public bool VolunteerOpp { get; set; }
        [Display(Name = "Інвестори?")]
        public bool InvestOpp { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy hh:mm tt}", ApplyFormatInEditMode = true)]
        [Display(Name = "Початок")]
        public DateTime? StartDate { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Кінець")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy hh:mm tt}", ApplyFormatInEditMode = true)]
        public DateTime? FinishDate { get; set; }
        [Required]
        [MinLength(5)]
        [MaxLength(128)]
        [Display(Name = "Короткий опис")]
        public string ShortDesc { get; set; }
        [Required]
        [MinLength(5)]
        [MaxLength(4096)]
        [Display(Name = "Опис")]
        public string Description { get; set; }
        [Display(Name = "Фото")]
        public string MainImg { get; set; }
        [Required]
        [Display(Name = "Місце")]
        public string Location { get; set; }
        [Display(Name = "На карті")]
        public string LatLng { get; set; }
        [Display(Name = "Соціальні мережі")]
        public string SocialLinks { get; set; }

        [Required]
        [Display(Name = "Статус")]
        public string Status { get; set; }

        [Display(Name = "У слайдер?")]
        public bool Featured { get; set; }

        public virtual ICollection<Image> Images { get; set; }
        public virtual ICollection<InvestOpportunity> InvestOpportunities { get; set; }
        public virtual ICollection<VolunteerOpportunity> VolunteerOpportunities { get; set; }
        [Display(Name = "Відповідальний")]
        public virtual Member Member { get; set; }
        public ICollection<ProjectSocials> ProjectSocials { get; set; }

    }
}
