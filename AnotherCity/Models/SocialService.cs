using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AnotherCity.Models
{
    public class SocialService
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        [Url]
        public string BaseLink { get; set; }

        public ICollection<MemberSocials> MemberSocials { get; set; }
    }

    public class MemberSocials
    {
        public int Id { get; set; }
        [Required]
        public int MemberId { get; set; }
        [Required]
        public int SocialId { get; set; }
        [Required]
        [Url]
        public string PersonalLink { get; set; }

        public virtual Member Member { get; set; }
        public virtual SocialService Social { get; set; }

    }
    public class ProjectSocials
    {
        public int Id { get; set; }
        [Required]
        public int ProjectId { get; set; }
        [Required]
        [Display(Name = "Social Service")]
        public int SocialId { get; set; }
        [Display(Name = "Followers #")]
        public int Followers { get; set; }
        [Required]
        [Url]
        [Display(Name = "URL")]
        public string ProjectLink { get; set; }

        public virtual Project Project { get; set; }
        public virtual SocialService Social { get; set; }

    }
}
