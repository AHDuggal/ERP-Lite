using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERPLite.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AuditRefactorMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "Organizations");

            migrationBuilder.DropColumn(
                name: "ModifiedOnUtc",
                table: "Organizations");

            migrationBuilder.AlterColumn<Guid>(
                name: "CreatedBy",
                table: "Organizations",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<Guid>(
                name: "DeletedBy",
                table: "Organizations",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedBy",
                table: "Organizations",
                type: "uniqueidentifier",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "Organizations");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Organizations");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Organizations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "Organizations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOnUtc",
                table: "Organizations",
                type: "datetime2",
                nullable: true);
        }
    }
}
