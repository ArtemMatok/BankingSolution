using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankingSolutionWebApi.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Update_BankingAccount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "666dfefc-2011-4a18-bab0-b1eaefa07713");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "04dd8420-c327-4616-8fcd-ed06ecb72153", null, "User", "USER" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "04dd8420-c327-4616-8fcd-ed06ecb72153");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "666dfefc-2011-4a18-bab0-b1eaefa07713", null, "User", "USER" });
        }
    }
}
