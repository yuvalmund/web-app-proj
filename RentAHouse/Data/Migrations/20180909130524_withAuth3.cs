using Microsoft.EntityFrameworkCore.Migrations;

namespace RentAHouse.Data.Migrations
{
    public partial class withAuth3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Apartment_AspNetUsers_ApartmentOwnerId",
                table: "Apartment");

            migrationBuilder.RenameColumn(
                name: "ApartmentOwnerId",
                table: "Apartment",
                newName: "ownerId");

            migrationBuilder.RenameIndex(
                name: "IX_Apartment_ApartmentOwnerId",
                table: "Apartment",
                newName: "IX_Apartment_ownerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Apartment_AspNetUsers_ownerId",
                table: "Apartment",
                column: "ownerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Apartment_AspNetUsers_ownerId",
                table: "Apartment");

            migrationBuilder.RenameColumn(
                name: "ownerId",
                table: "Apartment",
                newName: "ApartmentOwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_Apartment_ownerId",
                table: "Apartment",
                newName: "IX_Apartment_ApartmentOwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Apartment_AspNetUsers_ApartmentOwnerId",
                table: "Apartment",
                column: "ApartmentOwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
