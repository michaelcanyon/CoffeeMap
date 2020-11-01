using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoffeeMapServer.Migrations
{
    public partial class addPics : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Picture",
                table:"RoasterRequests",
                nullable: true
                );

            migrationBuilder.AddColumn<byte[]>(
                name: "Picture",
                table:"Roasters",
                nullable: true
                );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Picture",
                table: "RoasterRequests"
                );

            migrationBuilder.AddColumn<byte[]>(
                name: "Picture",
                table: "Roasters"
                );
        }
    }
}
