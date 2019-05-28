using Microsoft.EntityFrameworkCore.Migrations;

namespace Reservation.Identity.Data.Migrations
{
    public partial class spGetRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c21c91c0-5c2f-45cc-ab6d-1d256538a4ee",
                column: "SecurityStamp",
                value: "63ddbf46-99a5-4bdd-b3c6-31ee0aec51ba");

            var sp = @"CREATE PROCEDURE [dbo].[GetRoles]
                    @RoleName Nvarchar(256)
                AS
                BEGIN
                    SET NOCOUNT ON;
                    select * from AspNetRoles where Name like @RoleName +'%' or @RoleName is null
                END";

            migrationBuilder.Sql(sp);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c21c91c0-5c2f-45cc-ab6d-1d256538a4ee",
                column: "SecurityStamp",
                value: "a5d4a1ec-5f6d-4120-b98c-7a8c0bde7e18");
        }
    }
}
