using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AnotherCity.Migrations
{
    public partial class VolInvFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_InvestOpportunities_OpportunityId",
                table: "User");

            migrationBuilder.DropForeignKey(
                name: "FK_User_VolunteerOpportunities_OpportunityId",
                table: "User");

            migrationBuilder.RenameColumn(
                name: "OpportunityId",
                table: "User",
                newName: "VolunteerOpportunityId");

            migrationBuilder.RenameIndex(
                name: "IX_User_OpportunityId",
                table: "User",
                newName: "IX_User_VolunteerOpportunityId");

            migrationBuilder.AddColumn<int>(
                name: "InvestOpportunityId",
                table: "User",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_InvestOpportunityId",
                table: "User",
                column: "InvestOpportunityId");

            migrationBuilder.AddForeignKey(
                name: "FK_User_InvestOpportunities_InvestOpportunityId",
                table: "User",
                column: "InvestOpportunityId",
                principalTable: "InvestOpportunities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_User_VolunteerOpportunities_VolunteerOpportunityId",
                table: "User",
                column: "VolunteerOpportunityId",
                principalTable: "VolunteerOpportunities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_InvestOpportunities_InvestOpportunityId",
                table: "User");

            migrationBuilder.DropForeignKey(
                name: "FK_User_VolunteerOpportunities_VolunteerOpportunityId",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_InvestOpportunityId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "InvestOpportunityId",
                table: "User");

            migrationBuilder.RenameColumn(
                name: "VolunteerOpportunityId",
                table: "User",
                newName: "OpportunityId");

            migrationBuilder.RenameIndex(
                name: "IX_User_VolunteerOpportunityId",
                table: "User",
                newName: "IX_User_OpportunityId");

            migrationBuilder.AddForeignKey(
                name: "FK_User_InvestOpportunities_OpportunityId",
                table: "User",
                column: "OpportunityId",
                principalTable: "InvestOpportunities",
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
    }
}
