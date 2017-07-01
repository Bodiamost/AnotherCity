using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AnotherCity.Migrations
{
    public partial class SocialAndPositionsAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_VolunteerOpportunities_OpportunityId",
                table: "User");

            migrationBuilder.AddColumn<int>(
                name: "PositioId",
                table: "User",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PositionId",
                table: "User",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Positions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Positions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SocialServices",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BaseLink = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SocialServices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MembersSocials",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MemberId = table.Column<int>(nullable: false),
                    SocialId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MembersSocials", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MembersSocials_User_MemberId",
                        column: x => x.MemberId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MembersSocials_SocialServices_SocialId",
                        column: x => x.SocialId,
                        principalTable: "SocialServices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_User_PositioId",
                table: "User",
                column: "PositioId");

            migrationBuilder.CreateIndex(
                name: "IX_MembersSocials_MemberId",
                table: "MembersSocials",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_MembersSocials_SocialId",
                table: "MembersSocials",
                column: "SocialId");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Positions_PositioId",
                table: "User",
                column: "PositioId",
                principalTable: "Positions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_User_VolunteerOpportunities_OpportunityId",
                table: "User",
                column: "OpportunityId",
                principalTable: "VolunteerOpportunities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Positions_PositioId",
                table: "User");

            migrationBuilder.DropForeignKey(
                name: "FK_User_VolunteerOpportunities_OpportunityId",
                table: "User");

            migrationBuilder.DropTable(
                name: "MembersSocials");

            migrationBuilder.DropTable(
                name: "Positions");

            migrationBuilder.DropTable(
                name: "SocialServices");

            migrationBuilder.DropIndex(
                name: "IX_User_PositioId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "PositioId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "PositionId",
                table: "User");

            migrationBuilder.AddForeignKey(
                name: "FK_User_VolunteerOpportunities_OpportunityId",
                table: "User",
                column: "OpportunityId",
                principalTable: "VolunteerOpportunities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
