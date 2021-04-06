using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace jwtapitoken.Migrations
{
    public partial class addimgtomodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "ImgProfile",
                table: "AspNetUsers",
                type: "varbinary(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImgProfile",
                table: "AspNetUsers");
        }
    }
}
