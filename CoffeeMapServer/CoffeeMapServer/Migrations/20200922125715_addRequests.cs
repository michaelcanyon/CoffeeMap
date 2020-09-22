using Microsoft.EntityFrameworkCore.Migrations;

namespace CoffeeMapServer.Migrations
{
    public partial class addRequests : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateTable(
                name: "RoasterRequests",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    ContactNumber = table.Column<string>(nullable: false),
                    ContactEmail = table.Column<string>(nullable: false),
                    WebSiteLink = table.Column<string>(nullable: true),
                    VkProfileLink = table.Column<string>(nullable: true),
                    InstagramProfileLink = table.Column<string>(nullable: true),
                    TelegramProfileLink = table.Column<string>(nullable: true),
                    TagString = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    AddressStr = table.Column<string>(nullable: false),
                    OpeningHours = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoasterRequests", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropTable(
                name: "RoasterRequests");
        }
    }
}
