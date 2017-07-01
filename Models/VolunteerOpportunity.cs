using System;
using System.Collections.Generic;

namespace AnotherCity.Models
{
    public partial class VolunteerOpportunity
    {
        public VolunteerOpportunity()
        {
            Volunteers = new HashSet<Volunteer>();
        }

        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string Title { get; set; }
        public string JobType { get; set; }
        public string Description { get; set; }
        public string Reward { get; set; }

        public virtual ICollection<Volunteer> Volunteers { get; set; }
        public virtual Project Project { get; set; }
    }
}
