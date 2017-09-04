using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AnotherCity.Migrations
{
    public partial class ProjectDesc_lenght4096 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Projects",
                maxLength: 4096,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 1024);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Projects",
                maxLength: 1024,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 4096);
        }
    }
}
