using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoViajes.API.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "UserEntity",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    first_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    last_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserEntity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "destinations",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: false),
                    description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    location = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    image_url = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CreatedByUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UpdatedByUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    created_by = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_by = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    updated_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_destinations", x => x.id);
                    table.ForeignKey(
                        name: "FK_destinations_UserEntity_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "UserEntity",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_destinations_UserEntity_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "UserEntity",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "types_flight",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: false),
                    CreatedByUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UpdatedByUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    created_by = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_by = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    updated_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_types_flight", x => x.id);
                    table.ForeignKey(
                        name: "FK_types_flight_UserEntity_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "UserEntity",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_types_flight_UserEntity_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "UserEntity",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "types_hosting",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: false),
                    CreatedByUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UpdatedByUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    created_by = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_by = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    updated_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_types_hosting", x => x.id);
                    table.ForeignKey(
                        name: "FK_types_hosting_UserEntity_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "UserEntity",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_types_hosting_UserEntity_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "UserEntity",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "points_interest",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: false),
                    description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    image_url = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    destination_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UpdatedByUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    created_by = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_by = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    updated_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_points_interest", x => x.id);
                    table.ForeignKey(
                        name: "FK_points_interest_UserEntity_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "UserEntity",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_points_interest_UserEntity_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "UserEntity",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_points_interest_destinations_destination_id",
                        column: x => x.destination_id,
                        principalSchema: "dbo",
                        principalTable: "destinations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "travel_packages",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    duration = table.Column<int>(type: "int", nullable: false),
                    number_person = table.Column<int>(type: "int", nullable: false),
                    image_url = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    destination_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UpdatedByUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    created_by = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_by = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    updated_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_travel_packages", x => x.id);
                    table.ForeignKey(
                        name: "FK_travel_packages_UserEntity_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "UserEntity",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_travel_packages_UserEntity_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "UserEntity",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_travel_packages_destinations_destination_id",
                        column: x => x.destination_id,
                        principalSchema: "dbo",
                        principalTable: "destinations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
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
                    CreatedByUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UpdatedByUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    created_by = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_by = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    updated_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_flights", x => x.id);
                    table.ForeignKey(
                        name: "FK_flights_UserEntity_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "UserEntity",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_flights_UserEntity_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "UserEntity",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_flights_destinations_destination_id",
                        column: x => x.destination_id,
                        principalSchema: "dbo",
                        principalTable: "destinations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_flights_types_flight_type_flight_id",
                        column: x => x.type_flight_id,
                        principalSchema: "dbo",
                        principalTable: "types_flight",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "hostings",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    hosting_type_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    destination_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: false),
                    description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    price_per_night = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UpdatedByUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    created_by = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_by = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    updated_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hostings", x => x.id);
                    table.ForeignKey(
                        name: "FK_hostings_UserEntity_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "UserEntity",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_hostings_UserEntity_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "UserEntity",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_hostings_destinations_destination_id",
                        column: x => x.destination_id,
                        principalSchema: "dbo",
                        principalTable: "destinations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_hostings_types_hosting_hosting_type_id",
                        column: x => x.hosting_type_id,
                        principalSchema: "dbo",
                        principalTable: "types_hosting",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "activities",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    travel_package_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UpdatedByUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    created_by = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_by = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    updated_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_activities", x => x.id);
                    table.ForeignKey(
                        name: "FK_activities_UserEntity_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "UserEntity",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_activities_UserEntity_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "UserEntity",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_activities_travel_packages_travel_package_id",
                        column: x => x.travel_package_id,
                        principalSchema: "dbo",
                        principalTable: "travel_packages",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "assessments",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    travel_package_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    stars = table.Column<int>(type: "int", nullable: false),
                    comment = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    user_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UpdatedByUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    created_by = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_by = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    updated_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_assessments", x => x.id);
                    table.ForeignKey(
                        name: "FK_assessments_UserEntity_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "UserEntity",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_assessments_UserEntity_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "UserEntity",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_assessments_UserEntity_user_id",
                        column: x => x.user_id,
                        principalTable: "UserEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_assessments_travel_packages_travel_package_id",
                        column: x => x.travel_package_id,
                        principalSchema: "dbo",
                        principalTable: "travel_packages",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                    user_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UpdatedByUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    created_by = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    created_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_by = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    updated_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reservations", x => x.id);
                    table.ForeignKey(
                        name: "FK_reservations_UserEntity_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "UserEntity",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_reservations_UserEntity_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "UserEntity",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_reservations_UserEntity_user_id",
                        column: x => x.user_id,
                        principalTable: "UserEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                name: "IX_activities_CreatedByUserId",
                schema: "dbo",
                table: "activities",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_activities_travel_package_id",
                schema: "dbo",
                table: "activities",
                column: "travel_package_id");

            migrationBuilder.CreateIndex(
                name: "IX_activities_UpdatedByUserId",
                schema: "dbo",
                table: "activities",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_assessments_CreatedByUserId",
                schema: "dbo",
                table: "assessments",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_assessments_travel_package_id",
                schema: "dbo",
                table: "assessments",
                column: "travel_package_id");

            migrationBuilder.CreateIndex(
                name: "IX_assessments_UpdatedByUserId",
                schema: "dbo",
                table: "assessments",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_assessments_user_id",
                schema: "dbo",
                table: "assessments",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_destinations_CreatedByUserId",
                schema: "dbo",
                table: "destinations",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_destinations_UpdatedByUserId",
                schema: "dbo",
                table: "destinations",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_flights_CreatedByUserId",
                schema: "dbo",
                table: "flights",
                column: "CreatedByUserId");

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
                name: "IX_flights_UpdatedByUserId",
                schema: "dbo",
                table: "flights",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_hostings_CreatedByUserId",
                schema: "dbo",
                table: "hostings",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_hostings_destination_id",
                schema: "dbo",
                table: "hostings",
                column: "destination_id");

            migrationBuilder.CreateIndex(
                name: "IX_hostings_hosting_type_id",
                schema: "dbo",
                table: "hostings",
                column: "hosting_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_hostings_UpdatedByUserId",
                schema: "dbo",
                table: "hostings",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_points_interest_CreatedByUserId",
                schema: "dbo",
                table: "points_interest",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_points_interest_destination_id",
                schema: "dbo",
                table: "points_interest",
                column: "destination_id");

            migrationBuilder.CreateIndex(
                name: "IX_points_interest_UpdatedByUserId",
                schema: "dbo",
                table: "points_interest",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_reservations_CreatedByUserId",
                schema: "dbo",
                table: "reservations",
                column: "CreatedByUserId");

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

            migrationBuilder.CreateIndex(
                name: "IX_reservations_UpdatedByUserId",
                schema: "dbo",
                table: "reservations",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_reservations_user_id",
                schema: "dbo",
                table: "reservations",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_travel_packages_CreatedByUserId",
                schema: "dbo",
                table: "travel_packages",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_travel_packages_destination_id",
                schema: "dbo",
                table: "travel_packages",
                column: "destination_id");

            migrationBuilder.CreateIndex(
                name: "IX_travel_packages_UpdatedByUserId",
                schema: "dbo",
                table: "travel_packages",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_types_flight_CreatedByUserId",
                schema: "dbo",
                table: "types_flight",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_types_flight_UpdatedByUserId",
                schema: "dbo",
                table: "types_flight",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_types_hosting_CreatedByUserId",
                schema: "dbo",
                table: "types_hosting",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_types_hosting_UpdatedByUserId",
                schema: "dbo",
                table: "types_hosting",
                column: "UpdatedByUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "activities",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "assessments",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "points_interest",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "reservations",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "flights",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "hostings",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "travel_packages",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "types_flight",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "types_hosting",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "destinations",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "UserEntity");
        }
    }
}
