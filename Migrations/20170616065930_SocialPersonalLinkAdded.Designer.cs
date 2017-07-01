using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using AnotherCity.Data;

namespace AnotherCity.Migrations
{
    [DbContext(typeof(AnotherCityDbContext))]
    [Migration("20170616065930_SocialPersonalLinkAdded")]
    partial class SocialPersonalLinkAdded
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AnotherCity.Models.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PasswordSalt");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("AnotherCity.Models.Image", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<string>("Path");

                    b.Property<int>("ProjectId");

                    b.Property<double?>("Size");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("AnotherCity.Models.InvestOpportunity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("Amount");

                    b.Property<string>("BudgetDocument");

                    b.Property<string>("Description");

                    b.Property<string>("LinkToInvest");

                    b.Property<int>("ProjectId");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.ToTable("InvestOpportunities");
                });

            modelBuilder.Entity("AnotherCity.Models.MemberSocials", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("MemberId");

                    b.Property<string>("PersonalLink");

                    b.Property<int>("SocialId");

                    b.HasKey("Id");

                    b.HasIndex("MemberId");

                    b.HasIndex("SocialId");

                    b.ToTable("MembersSocials");
                });

            modelBuilder.Entity("AnotherCity.Models.Positions", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("Positions");
                });

            modelBuilder.Entity("AnotherCity.Models.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .HasMaxLength(1024);

                    b.Property<DateTime?>("FinishDate");

                    b.Property<bool>("InvestOpp");

                    b.Property<string>("LatLng");

                    b.Property<string>("Location");

                    b.Property<string>("MainImg");

                    b.Property<int?>("MemberId");

                    b.Property<string>("ShortDesc")
                        .HasMaxLength(128);

                    b.Property<string>("SocialLinks");

                    b.Property<DateTime?>("StartDate");

                    b.Property<string>("Status");

                    b.Property<string>("Title");

                    b.Property<bool>("VolunteerOpp");

                    b.HasKey("Id");

                    b.HasIndex("MemberId");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("AnotherCity.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("AnotherCity.Models.SocialService", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BaseLink");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("SocialServices");
                });

            modelBuilder.Entity("AnotherCity.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("AccountId");

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<string>("Email");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<string>("Phone");

                    b.HasKey("Id");

                    b.HasIndex("AccountId")
                        .IsUnique();

                    b.ToTable("User");

                    b.HasDiscriminator<string>("Discriminator").HasValue("User");
                });

            modelBuilder.Entity("AnotherCity.Models.VolunteerOpportunity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("JobType");

                    b.Property<int>("ProjectId");

                    b.Property<string>("Reward");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.ToTable("VolunteerOpportunities");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<int>("RoleId");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<int>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<int>("UserId");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<int>", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<int>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserToken<int>", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("AnotherCity.Models.Investor", b =>
                {
                    b.HasBaseType("AnotherCity.Models.User");

                    b.Property<int?>("OpportunityId");

                    b.HasIndex("OpportunityId");

                    b.ToTable("Investor");

                    b.HasDiscriminator().HasValue("Investor");
                });

            modelBuilder.Entity("AnotherCity.Models.Member", b =>
                {
                    b.HasBaseType("AnotherCity.Models.User");

                    b.Property<string>("Bio")
                        .HasMaxLength(1024);

                    b.Property<string>("PhotoImg");

                    b.Property<int>("PositionId");

                    b.Property<bool>("TopMember");

                    b.HasIndex("PositionId");

                    b.ToTable("Member");

                    b.HasDiscriminator().HasValue("Member");
                });

            modelBuilder.Entity("AnotherCity.Models.Volunteer", b =>
                {
                    b.HasBaseType("AnotherCity.Models.User");

                    b.Property<string>("Bio")
                        .HasMaxLength(1024);

                    b.Property<int?>("OpportunityId");

                    b.Property<string>("SocialLink");

                    b.HasIndex("OpportunityId");

                    b.ToTable("Volunteer");

                    b.HasDiscriminator().HasValue("Volunteer");
                });

            modelBuilder.Entity("AnotherCity.Models.Image", b =>
                {
                    b.HasOne("AnotherCity.Models.Project", "Project")
                        .WithMany("Images")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AnotherCity.Models.InvestOpportunity", b =>
                {
                    b.HasOne("AnotherCity.Models.Project", "Project")
                        .WithMany("InvestOpportunities")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AnotherCity.Models.MemberSocials", b =>
                {
                    b.HasOne("AnotherCity.Models.Member", "Member")
                        .WithMany()
                        .HasForeignKey("MemberId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AnotherCity.Models.SocialService", "Social")
                        .WithMany()
                        .HasForeignKey("SocialId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AnotherCity.Models.Project", b =>
                {
                    b.HasOne("AnotherCity.Models.Member", "Member")
                        .WithMany("Projects")
                        .HasForeignKey("MemberId");
                });

            modelBuilder.Entity("AnotherCity.Models.User", b =>
                {
                    b.HasOne("AnotherCity.Models.Account", "Account")
                        .WithOne("User")
                        .HasForeignKey("AnotherCity.Models.User", "AccountId");
                });

            modelBuilder.Entity("AnotherCity.Models.VolunteerOpportunity", b =>
                {
                    b.HasOne("AnotherCity.Models.Project", "Project")
                        .WithMany("VolunteerOpportunities")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<int>", b =>
                {
                    b.HasOne("AnotherCity.Models.Role")
                        .WithMany("Claims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<int>", b =>
                {
                    b.HasOne("AnotherCity.Models.Account")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<int>", b =>
                {
                    b.HasOne("AnotherCity.Models.Account")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<int>", b =>
                {
                    b.HasOne("AnotherCity.Models.Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AnotherCity.Models.Account")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AnotherCity.Models.Investor", b =>
                {
                    b.HasOne("AnotherCity.Models.InvestOpportunity", "Opportunity")
                        .WithMany("Investors")
                        .HasForeignKey("OpportunityId");
                });

            modelBuilder.Entity("AnotherCity.Models.Member", b =>
                {
                    b.HasOne("AnotherCity.Models.Positions", "Position")
                        .WithMany()
                        .HasForeignKey("PositionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AnotherCity.Models.Volunteer", b =>
                {
                    b.HasOne("AnotherCity.Models.VolunteerOpportunity", "Opportunity")
                        .WithMany("Volunteers")
                        .HasForeignKey("OpportunityId");
                });
        }
    }
}
