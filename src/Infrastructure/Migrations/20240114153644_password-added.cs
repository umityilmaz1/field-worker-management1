﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanArchitecture.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class passwordadded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Accounts",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "Accounts");
        }
    }
}
