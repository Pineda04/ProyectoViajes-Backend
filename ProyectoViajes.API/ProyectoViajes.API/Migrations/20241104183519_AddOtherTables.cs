using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoViajes.API.Migrations
{
    /// <inheritdoc />
    public partial class AddOtherTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "types_hosting",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: false),
                    description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    created_by = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_by = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    updated_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_types_hosting", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "TypesFlight",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: false),
                    description = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: false),
                    created_by = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_by = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    updated_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypesFlight", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "hosting",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    hosting_type_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    destination_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: false),
                    description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    price_per_night = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    created_by = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_by = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    updated_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hosting", x => x.id);
                    table.ForeignKey(
                        name: "FK_hosting_destinations_destination_id",
                        column: x => x.destination_id,
                        principalSchema: "dbo",
                        principalTable: "destinations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_hosting_types_hosting_hosting_type_id",
                        column: x => x.hosting_type_id,
                        principalSchema: "dbo",
                        principalTable: "types_hosting",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "flights",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    type_flight_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    destination_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    airline = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: false),
                    origin = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    departure_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    arrival_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    created_by = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_by = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    updated_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_flights", x => x.id);
                    table.ForeignKey(
                        name: "FK_flights_TypesFlight_type_flight_id",
                        column: x => x.type_flight_id,
                        principalTable: "TypesFlight",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_flights_destinations_destination_id",
                        column: x => x.destination_id,
                        principalSchema: "dbo",
                        principalTable: "destinations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_flights_destination_id",
                schema: "dbo",
                table: "flights",
                column: "destination_id");

            migrationBuilder.CreateIndex(
                name: "IX_flights_type_flight_id",
                schema: "dbo",
                table: "flights",
                column: "type_flight_id");

            migrationBuilder.CreateIndex(
                name: "IX_hosting_destination_id",
                schema: "dbo",
                table: "hosting",
                column: "destination_id");

            migrationBuilder.CreateIndex(
                name: "IX_hosting_hosting_type_id",
                schema: "dbo",
                table: "hosting",
                column: "hosting_type_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "flights",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "hosting",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "TypesFlight");

            migrationBuilder.DropTable(
                name: "types_hosting",
                schema: "dbo");
        }
    }
}
