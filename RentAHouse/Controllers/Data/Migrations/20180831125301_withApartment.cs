using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RentAHouse.Data.Migrations
{
    public partial class withApartment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApartmentOwner",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    userName = table.Column<string>(nullable: true),
                    password = table.Column<string>(nullable: true),
                    firstName = table.Column<string>(nullable: true),
                    lastName = table.Column<string>(nullable: true),
                    mail = table.Column<string>(nullable: true),
                    rate = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApartmentOwner", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "City",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    cityName = table.Column<string>(nullable: true),
                    GraduatesPercents = table.Column<int>(nullable: false),
                    mayor = table.Column<string>(nullable: true),
                    avarageSalary = table.Column<int>(nullable: false),
                    numOfResidents = table.Column<int>(nullable: false),
                    region = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_City", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Apartment",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ownerID = table.Column<int>(nullable: true),
                    cityID = table.Column<int>(nullable: true),
                    street = table.Column<string>(nullable: true),
                    houseNumber = table.Column<int>(nullable: false),
                    roomsNumber = table.Column<int>(nullable: false),
                    size = table.Column<int>(nullable: false),
                    price = table.Column<int>(nullable: false),
                    cityTax = table.Column<int>(nullable: false),
                    BuildingTax = table.Column<int>(nullable: false),
                    furnitureInculded = table.Column<bool>(nullable: false),
                    isRenovatetd = table.Column<bool>(nullable: false),
                    arePetsAllowed = table.Column<bool>(nullable: false),
                    isThereElivator = table.Column<bool>(nullable: false),
                    EnterDate = table.Column<DateTime>(nullable: false),
                    floor = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Apartment", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Apartment_City_cityID",
                        column: x => x.cityID,
                        principalTable: "City",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Apartment_ApartmentOwner_ownerID",
                        column: x => x.ownerID,
                        principalTable: "ApartmentOwner",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ApartmentImage",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    apartmentID = table.Column<int>(nullable: true),
                    imageFileName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApartmentImage", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ApartmentImage_Apartment_apartmentID",
                        column: x => x.apartmentID,
                        principalTable: "Apartment",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Apartment_cityID",
                table: "Apartment",
                column: "cityID");

            migrationBuilder.CreateIndex(
                name: "IX_Apartment_ownerID",
                table: "Apartment",
                column: "ownerID");

            migrationBuilder.CreateIndex(
                name: "IX_ApartmentImage_apartmentID",
                table: "ApartmentImage",
                column: "apartmentID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApartmentImage");

            migrationBuilder.DropTable(
                name: "Apartment");

            migrationBuilder.DropTable(
                name: "City");

            migrationBuilder.DropTable(
                name: "ApartmentOwner");
        }
    }
}
