using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AnotherCity.Migrations
{
    public partial class SocialUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_VolunteerOpportunities_OpportunityId",
                table: "User");

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
                name: "FK_User_VolunteerOpportunities_OpportunityId",
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
