using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoViajes.API.Migrations
{
    /// <inheritdoc />
    public partial class FixTableFlights : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_hosting_destinations_destination_id",
                schema: "dbo",
                table: "hosting");

            migrationBuilder.DropForeignKey(
                name: "FK_hosting_types_hosting_hosting_type_id",
                schema: "dbo",
                table: "hosting");

            migrationBuilder.DropPrimaryKey(
                name: "PK_hosting",
                schema: "dbo",
                table: "hosting");

            migrationBuilder.RenameTable(
                name: "hosting",
                schema: "dbo",
                newName: "hostings",
                newSchema: "dbo");

            migrationBuilder.RenameIndex(
                name: "IX_hosting_hosting_type_id",
                schema: "dbo",
                table: "hostings",
                newName: "IX_hostings_hosting_type_id");

            migrationBuilder.RenameIndex(
                name: "IX_hosting_destination_id",
                schema: "dbo",
                table: "hostings",
                newName: "IX_hostings_destination_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_hostings",
                schema: "dbo",
                table: "hostings",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_hostings_destinations_destination_id",
                schema: "dbo",
                table: "hostings",
                column: "destination_id",
                principalSchema: "dbo",
                principalTable: "destinations",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_hostings_types_hosting_hosting_type_id",
                schema: "dbo",
                table: "hostings",
                column: "hosting_type_id",
                principalSchema: "dbo",
                principalTable: "types_hosting",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_hostings_destinations_destination_id",
                schema: "dbo",
                table: "hostings");

            migrationBuilder.DropForeignKey(
                name: "FK_hostings_types_hosting_hosting_type_id",
                schema: "dbo",
                table: "hostings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_hostings",
                schema: "dbo",
                table: "hostings");

            migrationBuilder.RenameTable(
                name: "hostings",
                schema: "dbo",
                newName: "hosting",
                newSchema: "dbo");

            migrationBuilder.RenameIndex(
                name: "IX_hostings_hosting_type_id",
                schema: "dbo",
                table: "hosting",
                newName: "IX_hosting_hosting_type_id");

            migrationBuilder.RenameIndex(
                name: "IX_hostings_destination_id",
                schema: "dbo",
                table: "hosting",
                newName: "IX_hosting_destination_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_hosting",
                schema: "dbo",
                table: "hosting",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_hosting_destinations_destination_id",
                schema: "dbo",
                table: "hosting",
                column: "destination_id",
                principalSchema: "dbo",
                principalTable: "destinations",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_hosting_types_hosting_hosting_type_id",
                schema: "dbo",
                table: "hosting",
                column: "hosting_type_id",
                principalSchema: "dbo",
                principalTable: "types_hosting",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
