using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Uber_Trash.Migrations
{
    /// <inheritdoc />
    public partial class AddedUserSellersToTheDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sellers",
                columns: table => new
                {
                    PublicKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone_Number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumberOfItemsToBeOrdered = table.Column<int>(type: "int", nullable: false),
                    Agent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Received = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sellers", x => x.PublicKey);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sellers");
        }
    }
}
