using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankingSolutionWebApi.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Add_BankingAccounts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b4f9ef8b-6eed-4a9f-ac9d-0112b44241ec");

            migrationBuilder.CreateTable(
                name: "BankingAccounts",
                columns: table => new
                {
                    BankingAccountId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Balance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CardNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankingAccounts", x => x.BankingAccountId);
                    table.ForeignKey(
                        name: "FK_BankingAccounts_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "666dfefc-2011-4a18-bab0-b1eaefa07713", null, "User", "USER" });

            migrationBuilder.CreateIndex(
                name: "IX_BankingAccounts_UserId",
                table: "BankingAccounts",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BankingAccounts");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "666dfefc-2011-4a18-bab0-b1eaefa07713");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b4f9ef8b-6eed-4a9f-ac9d-0112b44241ec", null, "User", "USER" });
        }
    }
}
