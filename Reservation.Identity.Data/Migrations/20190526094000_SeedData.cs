using Microsoft.EntityFrameworkCore.Migrations;

namespace Reservation.Identity.Data.Migrations
{
    public partial class SeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "IsBlock", "IsDeleted", "Name" },
                values: new object[] { "c21c91c0-5c2f-45cc-ab6d-1d256538a5ee", false, false, "Administrator" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "AdminId", "CityId", "ClientId", "CountryId", "Email", "EmailConfirmed", "EntityIdInfo", "Gender", "IsBlock", "IsDeleted", "LockoutEnabled", "LockoutEndDateUtc", "Name", "NameEn", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TelNumber", "TwoFactorEnabled", "UserName", "VendorId" },
                values: new object[] { "c21c91c0-5c2f-45cc-ab6d-1d256538a4ee", 0, null, null, null, null, null, "admin@A3n.com", false, null, null, false, false, false, null, null, null, "ALQ9yNzGkKdXRP8gdol1whMNSIZAlmjXpF6SNHELSKf0N6+aZs24+5h8B4OzpBWrIw==", "+9", false, "eb2cfd84-1e8a-4cf6-b50b-ed117ff108bf", null, false, "admin", null });

            migrationBuilder.InsertData(
                table: "AspNetUsersRoles",
                columns: new[] { "Id", "IsBlock", "IsDeleted", "RoleId", "UserId" },
                values: new object[] { "c21c91c0-5c2f-45cc-ab6d-1d256538a6ee", false, false, "c21c91c0-5c2f-45cc-ab6d-1d256538a5ee", "c21c91c0-5c2f-45cc-ab6d-1d256538a4ee" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsersRoles",
                keyColumn: "Id",
                keyValue: "c21c91c0-5c2f-45cc-ab6d-1d256538a6ee");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c21c91c0-5c2f-45cc-ab6d-1d256538a5ee");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c21c91c0-5c2f-45cc-ab6d-1d256538a4ee");
        }
    }
}
