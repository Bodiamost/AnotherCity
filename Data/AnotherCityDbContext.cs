using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AnotherCity.Models;

namespace AnotherCity.Data
{
    public class AnotherCityDbContext : IdentityDbContext<Account,Role,int>
    {
        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Image> Images { get; set; }
        public virtual DbSet<InvestOpportunity> InvestOpportunities { get; set; }
        public virtual DbSet<Investor> Investors { get; set; }
        public virtual DbSet<Member> Members { get; set; }
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<VolunteerOpportunity> VolunteerOpportunities { get; set; }
        public virtual DbSet<Volunteer> Volunteers { get; set; }
        public virtual DbSet<SocialService> SocialServices { get; set; }
        public virtual DbSet<MemberSocials> MembersSocials { get; set; }
        public virtual DbSet<ProjectSocials> ProjectsSocials { get; set; }
        public virtual DbSet<Positions> Positions { get; set; }



        public AnotherCityDbContext(DbContextOptions<AnotherCityDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
