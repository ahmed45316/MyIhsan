using Microsoft.EntityFrameworkCore.Migrations;

namespace Reservation.Identity.Data.Migrations
{
    public partial class MenuData2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c21c91c0-5c2f-45cc-ab6d-1d256538a4ee",
                column: "SecurityStamp",
                value: "85b831b9-dc43-4d66-af71-1dd9f4a81c6d");

            migrationBuilder.InsertData(
                table: "Menu",
                columns: new[] { "Id", "Action", "Controller", "Href", "Icon", "IsStop", "ItsOrder", "Parameters", "ParentId", "ScreenNameAr", "ScreenNameEn" },
                values: new object[] { "menu-1", "index", "Home", null, "icon-home", false, 1, null, null, "الشاشة الرئيسية", "Main Screen" });

            migrationBuilder.InsertData(
                table: "Menu",
                columns: new[] { "Id", "Action", "Controller", "Href", "Icon", "IsStop", "ItsOrder", "Parameters", "ParentId", "ScreenNameAr", "ScreenNameEn" },
                values: new object[] { "menu-2", null, null, null, "fas fa-address-card", false, 2, null, null, "الصلاحيات", "Authentication" });

            migrationBuilder.InsertData(
                table: "Menu",
                columns: new[] { "Id", "Action", "Controller", "Href", "Icon", "IsStop", "ItsOrder", "Parameters", "ParentId", "ScreenNameAr", "ScreenNameEn" },
                values: new object[] { "menu-7", "ManageRoles", "Security", null, "icon-user", false, 7, null, "menu-2", "الدور الوظيفي", "Roles" });

            migrationBuilder.InsertData(
                table: "Menu",
                columns: new[] { "Id", "Action", "Controller", "Href", "Icon", "IsStop", "ItsOrder", "Parameters", "ParentId", "ScreenNameAr", "ScreenNameEn" },
                values: new object[] { "menu-8", "Users", "Security", null, "icon-user", false, 8, null, "menu-2", "المستخدمين", "Users" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Menu",
                keyColumn: "Id",
                keyValue: "menu-1");

            migrationBuilder.DeleteData(
                table: "Menu",
                keyColumn: "Id",
                keyValue: "menu-7");

            migrationBuilder.DeleteData(
                table: "Menu",
                keyColumn: "Id",
                keyValue: "menu-8");

            migrationBuilder.DeleteData(
                table: "Menu",
                keyColumn: "Id",
                keyValue: "menu-2");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c21c91c0-5c2f-45cc-ab6d-1d256538a4ee",
                column: "SecurityStamp",
                value: "89d8ee77-0d2c-42f6-84de-efd4c72f0c2e");
        }
    }
}
