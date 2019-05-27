using Microsoft.EntityFrameworkCore.Migrations;

namespace Reservation.Identity.Data.Migrations
{
    public partial class MenuData1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c21c91c0-5c2f-45cc-ab6d-1d256538a4ee",
                column: "SecurityStamp",
                value: "89d8ee77-0d2c-42f6-84de-efd4c72f0c2e");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c21c91c0-5c2f-45cc-ab6d-1d256538a4ee",
                column: "SecurityStamp",
                value: "edd6a87f-9b76-411c-9bcc-b18e65a6b61e");
        }
    }
}
