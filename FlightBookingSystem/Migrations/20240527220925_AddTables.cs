using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class AddTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "flight_booking_schema");

            migrationBuilder.CreateTable(
                name: "airports",
                schema: "flight_booking_schema",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Location = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_airports", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "companies",
                schema: "flight_booking_schema",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    RegistrationDetails = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_companies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "aircraft",
                schema: "flight_booking_schema",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Model = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Capacity = table.Column<int>(type: "integer", nullable: false),
                    CompanyId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_aircraft", x => x.Id);
                    table.ForeignKey(
                        name: "FK_aircraft_companies_CompanyId",
                        column: x => x.CompanyId,
                        principalSchema: "flight_booking_schema",
                        principalTable: "companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "users",
                schema: "flight_booking_schema",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Role = table.Column<int>(type: "integer", nullable: false),
                    Username = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    FirstName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    LastName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    CompanyId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_users_companies_CompanyId",
                        column: x => x.CompanyId,
                        principalSchema: "flight_booking_schema",
                        principalTable: "companies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "flights",
                schema: "flight_booking_schema",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FlightNumber = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    DepartureTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ArrivalTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DepartureCity = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    ArrivalCity = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    AircraftId = table.Column<int>(type: "integer", nullable: false),
                    OriginAirportId = table.Column<int>(type: "integer", nullable: false),
                    DestinationAirportId = table.Column<int>(type: "integer", nullable: false),
                    CompanyId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_flights", x => x.Id);
                    table.ForeignKey(
                        name: "FK_flights_aircraft_AircraftId",
                        column: x => x.AircraftId,
                        principalSchema: "flight_booking_schema",
                        principalTable: "aircraft",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_flights_airports_DestinationAirportId",
                        column: x => x.DestinationAirportId,
                        principalSchema: "flight_booking_schema",
                        principalTable: "airports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_flights_airports_OriginAirportId",
                        column: x => x.OriginAirportId,
                        principalSchema: "flight_booking_schema",
                        principalTable: "airports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_flights_companies_CompanyId",
                        column: x => x.CompanyId,
                        principalSchema: "flight_booking_schema",
                        principalTable: "companies",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "company_registration_requests",
                schema: "flight_booking_schema",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    CompanyId = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<bool>(type: "boolean", nullable: false),
                    RequestDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_company_registration_requests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_company_registration_requests_companies_CompanyId",
                        column: x => x.CompanyId,
                        principalSchema: "flight_booking_schema",
                        principalTable: "companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_company_registration_requests_users_UserId",
                        column: x => x.UserId,
                        principalSchema: "flight_booking_schema",
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "bookings",
                schema: "flight_booking_schema",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FlightId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    BookingDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bookings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_bookings_flights_FlightId",
                        column: x => x.FlightId,
                        principalSchema: "flight_booking_schema",
                        principalTable: "flights",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_bookings_users_UserId",
                        column: x => x.UserId,
                        principalSchema: "flight_booking_schema",
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_aircraft_CompanyId",
                schema: "flight_booking_schema",
                table: "aircraft",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_bookings_FlightId",
                schema: "flight_booking_schema",
                table: "bookings",
                column: "FlightId");

            migrationBuilder.CreateIndex(
                name: "IX_bookings_UserId",
                schema: "flight_booking_schema",
                table: "bookings",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_company_registration_requests_CompanyId",
                schema: "flight_booking_schema",
                table: "company_registration_requests",
                column: "CompanyId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_company_registration_requests_UserId",
                schema: "flight_booking_schema",
                table: "company_registration_requests",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_flights_AircraftId",
                schema: "flight_booking_schema",
                table: "flights",
                column: "AircraftId");

            migrationBuilder.CreateIndex(
                name: "IX_flights_CompanyId",
                schema: "flight_booking_schema",
                table: "flights",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_flights_DestinationAirportId",
                schema: "flight_booking_schema",
                table: "flights",
                column: "DestinationAirportId");

            migrationBuilder.CreateIndex(
                name: "IX_flights_OriginAirportId",
                schema: "flight_booking_schema",
                table: "flights",
                column: "OriginAirportId");

            migrationBuilder.CreateIndex(
                name: "IX_users_CompanyId",
                schema: "flight_booking_schema",
                table: "users",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_users_Email",
                schema: "flight_booking_schema",
                table: "users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_users_Username",
                schema: "flight_booking_schema",
                table: "users",
                column: "Username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "bookings",
                schema: "flight_booking_schema");

            migrationBuilder.DropTable(
                name: "company_registration_requests",
                schema: "flight_booking_schema");

            migrationBuilder.DropTable(
                name: "flights",
                schema: "flight_booking_schema");

            migrationBuilder.DropTable(
                name: "users",
                schema: "flight_booking_schema");

            migrationBuilder.DropTable(
                name: "aircraft",
                schema: "flight_booking_schema");

            migrationBuilder.DropTable(
                name: "airports",
                schema: "flight_booking_schema");

            migrationBuilder.DropTable(
                name: "companies",
                schema: "flight_booking_schema");
        }
    }
}
