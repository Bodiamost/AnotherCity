﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AnotherCity.Models
{
    public partial class Member : User
    {
        public Member()
        {
            Projects = new HashSet<Project>();
        }

        [Required]
        [Display(Name = "Позиція")]
        public int PositionId { get; set; }
        [Required]
        [MinLength(5)]
        [MaxLength(1024)]
        [Display(Name = "Біографія")]
        public string Bio { get; set; }
        [Display(Name = "Фото")]
        public string PhotoImg { get; set; }
        [Display(Name = "Учасник?")]
        public bool TopMember { get; set; }

        public virtual ICollection<Project> Projects { get; set; }
        public virtual Positions Position { get; set; }
        public ICollection<MemberSocials> MemberSocials { get; set; }
    }
}
