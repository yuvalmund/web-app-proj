using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RentAHouse.Data.Migrations
{
    public partial class withAuth8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Apartment_ApartmentOwner_ownerID",
                table: "Apartment");

            migrationBuilder.DropTable(
                name: "ApartmentOwner");

            migrationBuilder.RenameColumn(
                name: "ownerID",
                table: "Apartment",
                newName: "ownerId");

            migrationBuilder.RenameIndex(
                name: "IX_Apartment_ownerID",
                table: "Apartment",
                newName: "IX_Apartment_ownerId");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "firstName",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "lastName",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "rate",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ownerId",
                table: "Apartment",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);

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

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "firstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "lastName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "rate",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "ownerId",
                table: "Apartment",
                newName: "ownerID");

            migrationBuilder.RenameIndex(
                name: "IX_Apartment_ownerId",
                table: "Apartment",
                newName: "IX_Apartment_ownerID");

            migrationBuilder.AlterColumn<int>(
                name: "ownerID",
                table: "Apartment",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "ApartmentOwner",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    firstName = table.Column<string>(nullable: true),
                    lastName = table.Column<string>(nullable: true),
                    rate = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApartmentOwner", x => x.ID);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Apartment_ApartmentOwner_ownerID",
                table: "Apartment",
                column: "ownerID",
                principalTable: "ApartmentOwner",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
