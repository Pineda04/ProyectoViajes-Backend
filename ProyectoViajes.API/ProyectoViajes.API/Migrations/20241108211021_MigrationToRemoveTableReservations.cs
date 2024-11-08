using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoViajes.API.Migrations
{
    /// <inheritdoc />
    public partial class MigrationToRemoveTableReservations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "reservations",
                schema: "dbo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "reservations",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    destination_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    flight_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    hosting_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    travel_package_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    created_by = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    end_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    start_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_by = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    updated_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    user_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reservations", x => x.id);
                    table.ForeignKey(
                        name: "FK_reservations_destinations_destination_id",
                        column: x => x.destination_id,
                        principalSchema: "dbo",
                        principalTable: "destinations",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_reservations_flights_flight_id",
                        column: x => x.flight_id,
                        principalSchema: "dbo",
                        principalTable: "flights",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_reservations_hostings_hosting_id",
                        column: x => x.hosting_id,
                        principalSchema: "dbo",
                        principalTable: "hostings",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_reservations_travel_packages_travel_package_id",
                        column: x => x.travel_package_id,
                        principalSchema: "dbo",
                        principalTable: "travel_packages",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_reservations_destination_id",
                schema: "dbo",
                table: "reservations",
                column: "destination_id");

            migrationBuilder.CreateIndex(
                name: "IX_reservations_flight_id",
                schema: "dbo",
                table: "reservations",
                column: "flight_id");

            migrationBuilder.CreateIndex(
                name: "IX_reservations_hosting_id",
                schema: "dbo",
                table: "reservations",
                column: "hosting_id");

            migrationBuilder.CreateIndex(
                name: "IX_reservations_travel_package_id",
                schema: "dbo",
                table: "reservations",
                column: "travel_package_id");
        }
    }
}
