using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoViajes.API.Migrations
{
    /// <inheritdoc />
    public partial class AddFieldsToUserTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "first_name",
                schema: "security",
                table: "users",
                type: "nvarchar(70)",
                maxLength: 70,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "last_name",
                schema: "security",
                table: "users",
                type: "nvarchar(70)",
                maxLength: 70,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "resfesh_token",
                schema: "security",
                table: "users",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "resfesh_token_expire",
                schema: "security",
                table: "users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<string>(
                name: "user_id",
                schema: "dbo",
                table: "reservations",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "user_id",
                schema: "dbo",
                table: "assessments",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_reservations_user_id",
                schema: "dbo",
                table: "reservations",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_assessments_user_id",
                schema: "dbo",
                table: "assessments",
                column: "user_id");

            migrationBuilder.AddForeignKey(
                name: "FK_assessments_users_user_id",
                schema: "dbo",
                table: "assessments",
                column: "user_id",
                principalSchema: "security",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_reservations_users_user_id",
                schema: "dbo",
                table: "reservations",
                column: "user_id",
                principalSchema: "security",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_assessments_users_user_id",
                schema: "dbo",
                table: "assessments");

            migrationBuilder.DropForeignKey(
                name: "FK_reservations_users_user_id",
                schema: "dbo",
                table: "reservations");

            migrationBuilder.DropIndex(
                name: "IX_reservations_user_id",
                schema: "dbo",
                table: "reservations");

            migrationBuilder.DropIndex(
                name: "IX_assessments_user_id",
                schema: "dbo",
                table: "assessments");

            migrationBuilder.DropColumn(
                name: "first_name",
                schema: "security",
                table: "users");

            migrationBuilder.DropColumn(
                name: "last_name",
                schema: "security",
                table: "users");

            migrationBuilder.DropColumn(
                name: "resfesh_token",
                schema: "security",
                table: "users");

            migrationBuilder.DropColumn(
                name: "resfesh_token_expire",
                schema: "security",
                table: "users");

            migrationBuilder.AlterColumn<string>(
                name: "user_id",
                schema: "dbo",
                table: "reservations",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "user_id",
                schema: "dbo",
                table: "assessments",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
