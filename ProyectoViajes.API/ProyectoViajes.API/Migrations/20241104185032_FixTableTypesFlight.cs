using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoViajes.API.Migrations
{
    /// <inheritdoc />
    public partial class FixTableTypesFlight : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_flights_TypesFlight_type_flight_id",
                schema: "dbo",
                table: "flights");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TypesFlight",
                table: "TypesFlight");

            migrationBuilder.RenameTable(
                name: "TypesFlight",
                newName: "types_flight",
                newSchema: "dbo");

            migrationBuilder.AddPrimaryKey(
                name: "PK_types_flight",
                schema: "dbo",
                table: "types_flight",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_flights_types_flight_type_flight_id",
                schema: "dbo",
                table: "flights",
                column: "type_flight_id",
                principalSchema: "dbo",
                principalTable: "types_flight",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_flights_types_flight_type_flight_id",
                schema: "dbo",
                table: "flights");

            migrationBuilder.DropPrimaryKey(
                name: "PK_types_flight",
                schema: "dbo",
                table: "types_flight");

            migrationBuilder.RenameTable(
                name: "types_flight",
                schema: "dbo",
                newName: "TypesFlight");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TypesFlight",
                table: "TypesFlight",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_flights_TypesFlight_type_flight_id",
                schema: "dbo",
                table: "flights",
                column: "type_flight_id",
                principalTable: "TypesFlight",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
