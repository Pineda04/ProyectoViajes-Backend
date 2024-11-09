using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoViajes.API.Migrations
{
    /// <inheritdoc />
    public partial class AddReservations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_flights_destinations_destination_id",
                schema: "dbo",
                table: "flights");

            migrationBuilder.DropForeignKey(
                name: "FK_flights_types_flight_type_flight_id",
                schema: "dbo",
                table: "flights");

            migrationBuilder.DropForeignKey(
                name: "FK_hostings_destinations_destination_id",
                schema: "dbo",
                table: "hostings");

            migrationBuilder.DropForeignKey(
                name: "FK_hostings_types_hosting_hosting_type_id",
                schema: "dbo",
                table: "hostings");

            migrationBuilder.DropForeignKey(
                name: "FK_travel_packages_destinations_destination_id",
                schema: "dbo",
                table: "travel_packages");

            migrationBuilder.CreateTable(
                name: "reservations",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    travel_package_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    flight_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    hosting_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    reservation_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    user_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    created_by = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_by = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    updated_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reservations", x => x.id);
                    table.ForeignKey(
                        name: "FK_reservations_flights_flight_id",
                        column: x => x.flight_id,
                        principalSchema: "dbo",
                        principalTable: "flights",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_reservations_hostings_hosting_id",
                        column: x => x.hosting_id,
                        principalSchema: "dbo",
                        principalTable: "hostings",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_reservations_travel_packages_travel_package_id",
                        column: x => x.travel_package_id,
                        principalSchema: "dbo",
                        principalTable: "travel_packages",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

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

            migrationBuilder.AddForeignKey(
                name: "FK_flights_destinations_destination_id",
                schema: "dbo",
                table: "flights",
                column: "destination_id",
                principalSchema: "dbo",
                principalTable: "destinations",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_flights_types_flight_type_flight_id",
                schema: "dbo",
                table: "flights",
                column: "type_flight_id",
                principalSchema: "dbo",
                principalTable: "types_flight",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_hostings_destinations_destination_id",
                schema: "dbo",
                table: "hostings",
                column: "destination_id",
                principalSchema: "dbo",
                principalTable: "destinations",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_hostings_types_hosting_hosting_type_id",
                schema: "dbo",
                table: "hostings",
                column: "hosting_type_id",
                principalSchema: "dbo",
                principalTable: "types_hosting",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_travel_packages_destinations_destination_id",
                schema: "dbo",
                table: "travel_packages",
                column: "destination_id",
                principalSchema: "dbo",
                principalTable: "destinations",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_flights_destinations_destination_id",
                schema: "dbo",
                table: "flights");

            migrationBuilder.DropForeignKey(
                name: "FK_flights_types_flight_type_flight_id",
                schema: "dbo",
                table: "flights");

            migrationBuilder.DropForeignKey(
                name: "FK_hostings_destinations_destination_id",
                schema: "dbo",
                table: "hostings");

            migrationBuilder.DropForeignKey(
                name: "FK_hostings_types_hosting_hosting_type_id",
                schema: "dbo",
                table: "hostings");

            migrationBuilder.DropForeignKey(
                name: "FK_travel_packages_destinations_destination_id",
                schema: "dbo",
                table: "travel_packages");

            migrationBuilder.DropTable(
                name: "reservations",
                schema: "dbo");

            migrationBuilder.AddForeignKey(
                name: "FK_flights_destinations_destination_id",
                schema: "dbo",
                table: "flights",
                column: "destination_id",
                principalSchema: "dbo",
                principalTable: "destinations",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_flights_types_flight_type_flight_id",
                schema: "dbo",
                table: "flights",
                column: "type_flight_id",
                principalSchema: "dbo",
                principalTable: "types_flight",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

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

            migrationBuilder.AddForeignKey(
                name: "FK_travel_packages_destinations_destination_id",
                schema: "dbo",
                table: "travel_packages",
                column: "destination_id",
                principalSchema: "dbo",
                principalTable: "destinations",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
